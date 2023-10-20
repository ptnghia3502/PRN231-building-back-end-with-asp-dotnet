using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories.Repository
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(FStoreDBContext context) : base(context)
        {
        }
    }
}
