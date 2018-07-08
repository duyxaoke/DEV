using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface ICurrencyAppService : IDisposable
    {
        void Create(CurrencyViewModel CurrencyViewModel);

        IEnumerable<CurrencyViewModel> GetAll();

        CurrencyViewModel GetById(Guid id);

        void Update(CurrencyViewModel CurrencyViewModel);

        void Remove(Guid id);

        IList<CurrencyHistoryData> GetAllHistory(Guid id);
    }
}