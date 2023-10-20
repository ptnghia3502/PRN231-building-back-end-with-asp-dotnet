using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Repositories;
using Repositories.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateOrder(OrderCreateView createDTO)
        {
            var newOrder = _mapper.Map<Order>(createDTO);

            await _unitOfWork.OrderRepo.AddAsync(newOrder);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _unitOfWork.OrderRepo.FindByField(x => x.OrderId == id);
            _unitOfWork.OrderRepo.Remove(order);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<List<OrderView>> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepo.GetAllAsync(x => x.Member!, x => x.OrderDetails);
            var result = _mapper.Map<List<OrderView>>(orders);
            return result;
        }

        public async Task<List<OrderView>> GetOrderstByMemberId(int memberId)
        {
            var orders = await _unitOfWork.OrderRepo.FindListByField(x => x.MemberId == memberId, x => x.Member!, x => x.OrderDetails);
            var result = _mapper.Map<List<OrderView>>(orders);
            return result;
        }

        public async Task<OrderView> GetOrdertById(int id)
        {
            var order = await _unitOfWork.OrderRepo.FindByField(x => x.OrderId == id, x => x.Member!, x => x.OrderDetails);
            var result = _mapper.Map<OrderView>(order);
            return result;
        }

        public async Task<bool> UpdateOrder(int id, OrderUpdateView updateDTO)
        {
            var order = await _unitOfWork.OrderRepo.FindByField(x => x.OrderId == id);
            if (order == null)
            {
                return false;
            }
            order = _mapper.Map(updateDTO, order);
            _unitOfWork.OrderRepo.Update(order);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
