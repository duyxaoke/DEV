﻿using Equinox.Infra.CrossCutting.Identity.Authorization;
using Equinox.Infra.CrossCutting.Identity.Data;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO;
using Equinox.WebApi.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using Equinox.WebApi.Extensions;
using System.Collections.Generic;
using Microsoft.AspNetCore.HttpOverrides;
using AspNetCoreRateLimit;

namespace Equinox.WebApi
{
    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }
        public static IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            HostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddWebApi(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                //options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
                options.UseCentralRoutePrefix(new RouteAttribute("api"));
            });

            services.AddPreRenderDebugging(HostingEnvironment);
            services.AddOptions();

            services.AddResponseCompression();

            services.AddCustomOpenIddict(HostingEnvironment);
            services.AddDistributedMemoryCache();
            services.AddCustomDbContext();

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
            services.RegisterCustomServices();

            services.AddAutoMapperSetup();
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

            //services.AddSwaggerGen(s =>
            //{
            //    s.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "Equinox Project",
            //        Description = "Equinox API Swagger surface",
            //        Contact = new Contact { Name = "Eduardo Pires", Email = "contato@eduardopires.net.br", Url = "http://www.eduardopires.net.br" },
            //        License = new License { Name = "MIT", Url = "https://github.com/EduardoPires/EquinoxProject/blob/master/LICENSE" }
            //    });
            //});

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IHttpContextAccessor accessor)
        {
            loggerFactory.AddConsole();
            //app.AddCustomSecurityHeaders();

            if (env.IsDevelopment())
            {
                app.AddDevMiddlewares();
            }
            else
            {
                app.UseHsts();
                app.UseResponseCompression();
            }

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            // https://github.com/openiddict/openiddict-core/issues/518
            // And
            // https://github.com/aspnet/Docs/issues/2384#issuecomment-297980490
            var forwarOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwarOptions.KnownNetworks.Clear();
            forwarOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwarOptions); 
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseIpRateLimiting();
            app.UseClientRateLimiting();


            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}