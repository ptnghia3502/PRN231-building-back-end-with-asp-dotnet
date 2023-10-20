using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderView>> GetAllOrders();
        Task<OrderView> GetOrdertById(int id);
        Task<List<OrderView>> GetOrderstByMemberId(int memberId);
        Task<bool> CreateOrder(OrderCreateView createDTO);
        Task<bool> UpdateOrder(int id, OrderUpdateView updateDTO);
        Task<bool> Delete(int id);
    }
}
