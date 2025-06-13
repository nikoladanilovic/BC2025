using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampaApp.Service.Common;
using BootcampApp.Repository.Common;

namespace BootcampApp.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
        public async Task<OrderModel?> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(OrderModel order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            await _orderRepository.AddAsync(order);
        }
        public async Task UpdateAsync(OrderModel order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            await _orderRepository.UpdateAsync(order);
        }
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid order ID", nameof(id));
            await _orderRepository.DeleteAsync(id);
        }
    }
}
