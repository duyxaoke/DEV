using System.Linq;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Equinox.Infra.Data.Repository
{
    public class RefferalRepository : Repository<Refferal>, IRefferalRepository
    {
        public RefferalRepository(EquinoxContext context)
            : base(context)
        {
        }
    }
}