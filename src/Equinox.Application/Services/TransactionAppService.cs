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
    public class TransactionAppService : ITransactionAppService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public TransactionAppService(IMapper mapper,
                                  ITransactionRepository TransactionRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _TransactionRepository = TransactionRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<TransactionViewModel> GetAll()
        {
            return _TransactionRepository.GetAll().ProjectTo<TransactionViewModel>();
        }

        public TransactionViewModel GetById(Guid id)
        {
            return _mapper.Map<TransactionViewModel>(_TransactionRepository.GetById(id));
        }

        public void Create(TransactionViewModel TransactionViewModel)
        {
            var registerCommand = _mapper.Map<CreateTransactionCommand>(TransactionViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(TransactionViewModel TransactionViewModel)
        {
            var updateCommand = _mapper.Map<UpdateTransactionCommand>(TransactionViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveTransactionCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<TransactionHistoryData> GetAllHistory(Guid id)
        {
            return TransactionHistory.ToJavaScriptTransactionHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}