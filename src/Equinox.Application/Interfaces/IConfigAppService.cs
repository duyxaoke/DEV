using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IConfigAppService : IDisposable
    {
        void Create(ConfigViewModel ConfigViewModel);

        IEnumerable<ConfigViewModel> GetAll();

        ConfigViewModel GetById(Guid id);

        void Update(ConfigViewModel ConfigViewModel);

        void Remove(Guid id);

        IList<ConfigHistoryData> GetAllHistory(Guid id);
    }
}