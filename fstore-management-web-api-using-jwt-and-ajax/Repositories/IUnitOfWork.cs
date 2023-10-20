using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;

namespace Repositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepo CategoryRepo { get; }
        public IMemberRepo MemberRepo { get; }
        public IOrderDetailRepo OrderDetailRepo { get; }
        public IOrderRepo OrderRepo { get; }
        public IProductRepo ProductRepo { get; }

        public Task<int> SaveChangeAsync();
    }
}
