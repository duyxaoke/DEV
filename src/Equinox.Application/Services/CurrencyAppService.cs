using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;

namespace Equinox.Application.Services
{
    public class CurrencyAppService : ICurrencyAppService
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _CurrencyRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CurrencyAppService(IMapper mapper,
                                  ICurrencyRepository CurrencyRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _CurrencyRepository = CurrencyRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CurrencyViewModel> GetAll()
        {
            return _CurrencyRepository.GetAll().ProjectTo<CurrencyViewModel>();
        }

        public CurrencyViewModel GetById(Guid id)
        {
            return _mapper.Map<CurrencyViewModel>(_CurrencyRepository.GetById(id));
        }

        public void Create(CurrencyViewModel CurrencyViewModel)
        {
            var registerCommand = _mapper.Map<CreateCurrencyCommand>(CurrencyViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CurrencyViewModel CurrencyViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCurrencyCommand>(CurrencyViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCurrencyCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CurrencyHistoryData> GetAllHistory(Guid id)
        {
            return CurrencyHistory.ToJavaScriptCurrencyHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}