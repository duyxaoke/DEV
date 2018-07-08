using System;
using System.Collections.Generic;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.ViewModels;

namespace Equinox.Application.Interfaces
{
    public interface IRefferalAppService : IDisposable
    {
        void Create(RefferalViewModel RefferalViewModel);

        IEnumerable<RefferalViewModel> GetAll();

        IEnumerable<RefferalViewModel> GetByUser(Guid userId);

        RefferalViewModel GetById(Guid id);

        void Remove(Guid id);

        IList<RefferalHistoryData> GetAllHistory(Guid id);
    }
}