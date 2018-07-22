using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNetCoreRateLimit;
using Equinox.API.Configurations;
using Equinox.API.Filters;
using Equinox.Infra.CrossCutting.Identity.Data;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Equinox.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            // Register the Identity services.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") &&
                       ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        ctx.Response.WriteAsync("{\"success\" : false, \"errors\" : \"Unauthorized\"}");
                    }
                    else
                    {
                        ctx.Response.Redirect(ctx.RedirectUri);
                    }
                    return Task.CompletedTask;
                };
            });

            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "560027070069-37ldt4kfuohhu3m495hk2j4pjp92d382.apps.googleusercontent.com";
                    options.ClientSecret = "n2Q-GEw9RQjzcRbU3qhfTj8f";
                })

                .AddTwitter(options =>
                {
                    options.ConsumerKey = "6XaCTaLbMqfj6ww3zvZ5g";
                    options.ConsumerSecret = "Il2eFzGIrYhz6BWjYhVXBPQSfZuS4xoHpSSyD9PI";
                });

            services.AddOpenIddict()

                // Register the OpenIddict core services.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>();
                })

                // Register the OpenIddict server handler.
                .AddServer(options =>
                {
                    // Register the ASP.NET Core MVC services used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.UseMvc();

                    // Enable the authorization, logout, token and userinfo endpoints.
                    options.EnableAuthorizationEndpoint("/connect/authorize")
                           .EnableLogoutEndpoint("/connect/logout")
                           .EnableTokenEndpoint("/connect/token")
                           .EnableUserinfoEndpoint("/api/userinfo");

                    // Note: the Mvc.Client sample only uses the code flow and the password flow, but you
                    // can enable the other flows if you need to support implicit or client credentials.
                    options.AllowAuthorizationCodeFlow()
                           .AllowPasswordFlow()
                           .AllowRefreshTokenFlow();

                    // Mark the "email", "profile" and "roles" scopes as supported scopes.
                    options.RegisterScopes(OpenIdConnectConstants.Scopes.Email,
                                           OpenIdConnectConstants.Scopes.Profile,
                                           OpenIddictConstants.Scopes.Roles);

                    // When request caching is enabled, authorization and logout requests
                    // are stored in the distributed cache by OpenIddict and the user agent
                    // is redirected to the same page with a single parameter (request_id).
                    // This allows flowing large OpenID Connect requests even when using
                    // an external authentication provider like Google, Facebook or Twitter.
                    options.EnableRequestCaching();

                    // During development, you can disable the HTTPS requirement.
                    options.DisableHttpsRequirement();

                    // Note: to use JWT access tokens instead of the default
                    // encrypted format, the following lines are required:
                    //
                    // options.UseJsonWebTokens();
                    // options.AddEphemeralSigningKey();

                    // Note: if you don't want to specify a client_id when sending
                    // a token or revocation request, uncomment the following line:
                    //
                    // options.AcceptAnonymousClients();

                    // Note: if you want to process authorization and token requests
                    // that specify non-registered scopes, uncomment the following line:
                    //
                    // options.DisableScopeValidation();

                    // Note: if you don't want to use permissions, you can disable
                    // permission enforcement by uncommenting the following lines:
                    //
                    // options.IgnoreEndpointPermissions()
                    //        .IgnoreGrantTypePermissions()
                    //        .IgnoreScopePermissions();
                })

                // Register the OpenIddict validation handler.
                // Note: the OpenIddict validation handler is only compatible with the
                // default token format or with reference tokens and cannot be used with
                // JWT tokens. For JWT tokens, use the Microsoft JWT bearer handler.
                .AddValidation();

            services.AddAutoMapperSetup();
            services.AddAuthorization();


            services.AddOptions();

            services.AddResponseCompression();
            services.AddMemoryCache();

            //configure ip rate limiting middle-ware
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //configure client rate limiting middleware
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            //services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            var opt = new ClientRateLimitOptions();
            ConfigurationBinder.Bind(Configuration.GetSection("ClientRateLimiting"), opt);

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver
                        = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
            //.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);

            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
            services.AddCors();

            // New instance every time, only configuration class needs so its ok
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<UserResolverService>();
            services.AddScoped<ApiExceptionFilter>();
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthEmailMessageSender>();
            


        }
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IHttpContextAccessor accessor)
        {
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:9000")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                //.builder.WithHeaders("Authorization")
                .AllowAnyMethod());

            loggerFactory.AddConsole();
             app.UseHsts();
             app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/error");

            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseIpRateLimiting();
            app.UseClientRateLimiting();

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            // Seed the database with the sample applications.
            // Note: in a real world application, this step should be part of a setup script.
            InitializeAsync(app.ApplicationServices).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync(IServiceProvider services)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();

                await CreateApplicationsAsync();
                await CreateScopesAsync();

                async Task CreateApplicationsAsync()
                {
                    var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                    if (await manager.FindByClientIdAsync("mvc") == null)
                    {
                        var descriptor = new OpenIddictApplicationDescriptor
                        {
                            ClientId = "mvc",
                            ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                            DisplayName = "MVC client application",
                            PostLogoutRedirectUris = { new Uri("http://localhost:9000/signout-callback-oidc") },
                            RedirectUris = { new Uri("http://localhost:9000/signin-oidc") },
                            Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles,
                             OpenIddictConstants.Permissions.Prefixes.Scope + "api",
                        }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                    //if (await manager.FindByClientIdAsync("api") == null)
                    //{
                    //    var descriptor = new OpenIddictApplicationDescriptor
                    //    {
                    //        ClientId = "api",
                    //        ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                    //        Permissions =
                    //        {
                    //            OpenIddictConstants.Permissions.Endpoints.Introspection
                    //        }
                    //    };

                    //    await manager.CreateAsync(descriptor);
                    //}
                }



                async Task CreateScopesAsync()
                {
                    var manager = scope.ServiceProvider.GetRequiredService<OpenIddictScopeManager<OpenIddictScope>>();

                    if (await manager.FindByNameAsync("api") == null)
                    {
                        var descriptor = new OpenIddictScopeDescriptor
                        {
                            Name = "api",
                            Resources = { "api" }
                        };

                        await manager.CreateAsync(descriptor);
                    }

                }
            }
        }
    }
}
