using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface ITransactionAppService : IDisposable
    {
        void Create(TransactionViewModel TransactionViewModel);

        IEnumerable<TransactionViewModel> GetAll();

        TransactionViewModel GetById(Guid id);

        void Update(TransactionViewModel TransactionViewModel);

        void Remove(Guid id);

        IList<TransactionHistoryData> GetAllHistory(Guid id);
    }
}