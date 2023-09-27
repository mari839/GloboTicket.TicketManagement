using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    internal class GetEventDetailQuery : IRequest<EventDetailVm>
    {
        public Guid Id { get; set; } // which event detail i need to fetch, what user must enter
    }
}
