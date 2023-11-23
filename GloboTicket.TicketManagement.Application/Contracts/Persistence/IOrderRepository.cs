using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<List<Order>> GetPadgedOrdersForMonth(DateTime date, int page, int size);

        Task<int> GetTotalCountOfOrdersForMonth(DateTime date);
    }
}
