﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    internal class GetEventListQuery : IRequest<List<EventListVm>>
    {
    }
}
