using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface IConfigRepository : IRepository<Config>
    {
        bool GetExist();
    }
}