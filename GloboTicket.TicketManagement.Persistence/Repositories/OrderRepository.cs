﻿using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GloboTicketDbContext globoTicketDbContext) : base(globoTicketDbContext)
        {
        }

        public async Task<List<Order>> GetPadgedOrdersForMonth(DateTime date, int page, int size)
        {
            return await _globoTicketDbContext.Orders.Where(x => x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
        {
            return await _globoTicketDbContext.Orders.CountAsync(x=> x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year); 
        }
    }
}
