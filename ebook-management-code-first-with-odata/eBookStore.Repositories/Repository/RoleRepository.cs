using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Domains.Models;
using eBookStore.Repositories.Data;
using eBookStore.Services.Interface;
using eBookStore.Services.InterfaceRepo;

namespace eBookStore.Repositories.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, IClaimsService claimsService, ICurrentTime currentTime) : base(dbContext, currentTime, claimsService)
        {

        }
    }
}
