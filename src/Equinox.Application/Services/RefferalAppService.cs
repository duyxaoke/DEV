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
    public class RefferalAppService : IRefferalAppService
    {
        private readonly IMapper _mapper;
        private readonly IRefferalRepository _RefferalRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public RefferalAppService(IMapper mapper,
                                  IRefferalRepository RefferalRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _RefferalRepository = RefferalRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<RefferalViewModel> GetAll()
        {
            return _RefferalRepository.GetAll().ProjectTo<RefferalViewModel>();
        }

        public IEnumerable<RefferalViewModel> GetByUser(Guid userId)
        {
            return _RefferalRepository.Get(c => c.UserId == userId).ProjectTo<RefferalViewModel>();
        }

        public RefferalViewModel GetById(Guid id)
        {
            return _mapper.Map<RefferalViewModel>(_RefferalRepository.GetById(id));
        }

        public void Create(RefferalViewModel RefferalViewModel)
        {
            var registerCommand = _mapper.Map<CreateRefferalCommand>(RefferalViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveRefferalCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<RefferalHistoryData> GetAllHistory(Guid id)
        {
            return RefferalHistory.ToJavaScriptRefferalHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}