using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Repositories.Models;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FStoreDBContext _dbContext;

        private readonly ICategoryRepo _categoryRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly IProductRepo _productRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;

        public UnitOfWork(FStoreDBContext dbContext, ICategoryRepo categoryRepo,
            IMemberRepo memberRepo,
            IProductRepo productRepo,
            IOrderRepo orderRepo,
            IOrderDetailRepo orderDetailRepo)
        {
            _dbContext = dbContext;
            _categoryRepo = categoryRepo;
            _memberRepo = memberRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
        }
        public ICategoryRepo CategoryRepo => _categoryRepo;
        public IMemberRepo MemberRepo => _memberRepo;
        public IProductRepo ProductRepo => _productRepo;
        public IOrderRepo OrderRepo => _orderRepo;
        public IOrderDetailRepo OrderDetailRepo => _orderDetailRepo;
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
