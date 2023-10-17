using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GloboTicket.TicketManagement.Persistence.Repositories
{
    internal class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboTicketDbContext context) : base(context) { }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime date)
        {
            var matches = _globoTicketDbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(date));
            return Task.FromResult(matches);
        }
    }
}
