using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;

namespace Equinox.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));

            CreateMap<ConfigViewModel, CreateConfigCommand>()
            .ConstructUsing(c => new CreateConfigCommand(c.SystemEnable, c.Currency, c.ReferalBonus));
            CreateMap<ConfigViewModel, UpdateConfigCommand>()
                .ConstructUsing(c => new UpdateConfigCommand(c.Id, c.SystemEnable, c.Currency, c.ReferalBonus));

            CreateMap<CurrencyViewModel, CreateCurrencyCommand>()
            .ConstructUsing(c => new CreateCurrencyCommand(c.Code, c.Name, c.Address, c.Quantity, c.IsActive));
            CreateMap<CurrencyViewModel, UpdateCurrencyCommand>()
                .ConstructUsing(c => new UpdateCurrencyCommand(c.Id, c.Code, c.Name, c.Address, c.Quantity, c.IsActive));

            CreateMap<RefferalViewModel, CreateRefferalCommand>()
            .ConstructUsing(c => new CreateRefferalCommand(c.UserId, c.UserRefId));

            CreateMap<TransactionViewModel, CreateTransactionCommand>()
            .ConstructUsing(c => new CreateTransactionCommand(c.UserId, c.DepWithType, c.Quantity, c.IP, c.Approve, c.CreatedDate, c.UpdatedDate));
            CreateMap<TransactionViewModel, UpdateTransactionCommand>()
                .ConstructUsing(c => new UpdateTransactionCommand(c.Id, c.UserId, c.DepWithType, c.Quantity, c.IP, c.Approve, c.CreatedDate, c.UpdatedDate));
        }
    }
}