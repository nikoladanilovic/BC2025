using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampaApp.Service.Common
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task<OrderModel?> GetByIdAsync(Guid id);
        Task AddAsync(OrderModel order);
        Task UpdateAsync(OrderModel order);
        Task DeleteAsync(Guid id);
    }
}
