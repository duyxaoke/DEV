using Equinox.Domain.Models;

namespace Equinox.Domain.Interfaces
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Currency GetByCode(string code);
    }
}