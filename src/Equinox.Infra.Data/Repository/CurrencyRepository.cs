using System.Linq;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(EquinoxContext context)
            : base(context)
        {
        }

        public Currency GetByCode(string code)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Code == code);
        }
    }
}