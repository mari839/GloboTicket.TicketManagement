using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);


            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request); //validate incoming command, it will trigger validation rules defined in our validator
            //validationResult will contain the lists of validation errors


            //if there are no validation rules throw custom ValidationException exception
            if(validationResult.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            //else add to database
            @event = await _eventRepository.AddAsync(@event);


            //Sending email notification to admin address
            var email = new Email() { To = "asff@gmail.com", Body = $"a new event was created: {request}", Subject = "a new event was created" };

            try
            {
                await _emailService.SendEmail(email);
            }catch (Exception ex)
            {
                //thi shouldn't stop the API from doing else so this can be logged
            }
            return @event.EventId;
        }
    }
}
