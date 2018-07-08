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
    public class ConfigAppService : IConfigAppService
    {
        private readonly IMapper _mapper;
        private readonly IConfigRepository _ConfigRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public ConfigAppService(IMapper mapper,
                                  IConfigRepository ConfigRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _ConfigRepository = ConfigRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<ConfigViewModel> GetAll()
        {
            return _ConfigRepository.GetAll().ProjectTo<ConfigViewModel>();
        }

        public ConfigViewModel GetById(Guid id)
        {
            return _mapper.Map<ConfigViewModel>(_ConfigRepository.GetById(id));
        }

        public void Create(ConfigViewModel ConfigViewModel)
        {
            var createCommand = _mapper.Map<CreateConfigCommand>(ConfigViewModel);
            Bus.SendCommand(createCommand);
        }

        public void Update(ConfigViewModel ConfigViewModel)
        {
            var updateCommand = _mapper.Map<UpdateConfigCommand>(ConfigViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveConfigCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<ConfigHistoryData> GetAllHistory(Guid id)
        {
            return ConfigHistory.ToJavaScriptConfigHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}