using Equinox.Application.Interfaces;
using Equinox.Application.Services;
using Equinox.Domain.CommandHandlers;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Events;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.EventHandlers;
using Equinox.Domain.Events;
using Equinox.Domain.Interfaces;
using Equinox.Infra.CrossCutting.Bus;
using Equinox.Infra.CrossCutting.Identity.Authorization;
using Equinox.Infra.CrossCutting.Identity.Data;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.Data.Context;
using Equinox.Infra.Data.EventSourcing;
using Equinox.Infra.Data.Repository;
using Equinox.Infra.Data.Repository.EventSourcing;
using Equinox.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IConfigAppService, ConfigAppService>();
            services.AddScoped<ICurrencyAppService, CurrencyAppService>();
            services.AddScoped<IRefferalAppService, RefferalAppService>();
            services.AddScoped<ITransactionAppService, TransactionAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            services.AddScoped<INotificationHandler<ConfigCreatedEvent>, ConfigEventHandler>();
            services.AddScoped<INotificationHandler<ConfigUpdatedEvent>, ConfigEventHandler>();
            services.AddScoped<INotificationHandler<ConfigRemovedEvent>, ConfigEventHandler>();

            services.AddScoped<INotificationHandler<CurrencyCreatedEvent>, CurrencyEventHandler>();
            services.AddScoped<INotificationHandler<CurrencyUpdatedEvent>, CurrencyEventHandler>();
            services.AddScoped<INotificationHandler<CurrencyRemovedEvent>, CurrencyEventHandler>();

            services.AddScoped<INotificationHandler<RefferalCreatedEvent>, RefferalEventHandler>();
            services.AddScoped<INotificationHandler<RefferalRemovedEvent>, RefferalEventHandler>();

            services.AddScoped<INotificationHandler<TransactionCreatedEvent>, TransactionEventHandler>();
            services.AddScoped<INotificationHandler<TransactionUpdatedEvent>, TransactionEventHandler>();
            services.AddScoped<INotificationHandler<TransactionRemovedEvent>, TransactionEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand>, CustomerCommandHandler>();

            services.AddScoped<IRequestHandler<CreateConfigCommand>, ConfigCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateConfigCommand>, ConfigCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveConfigCommand>, ConfigCommandHandler>();

            services.AddScoped<IRequestHandler<CreateCurrencyCommand>, CurrencyCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCurrencyCommand>, CurrencyCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCurrencyCommand>, CurrencyCommandHandler>();

            services.AddScoped<IRequestHandler<CreateRefferalCommand>, RefferalCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveRefferalCommand>, RefferalCommandHandler>();

            services.AddScoped<IRequestHandler<CreateTransactionCommand>, TransactionCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTransactionCommand>, TransactionCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTransactionCommand>, TransactionCommandHandler>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IConfigRepository, ConfigRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IRefferalRepository, RefferalRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EquinoxContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            //services.AddScoped<DbContextOptions>();
            //services.AddScoped<ApplicationDbContext>(); 
            //services.AddScoped<UserResolverService>();
            //services.AddScoped<IdentityOptions>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}