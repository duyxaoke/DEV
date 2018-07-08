using System.Linq;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
        public ConfigRepository(EquinoxContext context)
            : base(context)
        {
        }

        public bool GetExist()
        {
            return DbSet.AsNoTracking().Any();
        }
    }
}