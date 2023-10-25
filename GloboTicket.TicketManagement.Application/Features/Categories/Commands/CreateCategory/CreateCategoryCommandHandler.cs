using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //initialize what we must return, that inherits from BaseResponse class
            var createCategoryCommandResponse = new CreateCategoryCommandResponse();

            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request); //validate request

            //if there is any invalid input we return list of errors
            if(validationResult.Errors.Count > 0)
            {
                createCategoryCommandResponse.Success = false;
                createCategoryCommandResponse.ValidationErrors = new List<string>();
                foreach(var error in validationResult.Errors)
                {
                    createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            //else if request is valid and it's success it adds to database, maps with entity and return CreateCategoryCommandResponse CreateCategoryDto
            if (createCategoryCommandResponse.Success)
            {
                var category = new Category() { Name = request.Name };
                category = await _categoryRepository.AddAsync(category);
                createCategoryCommandResponse.Category = _mapper.Map<CreateCategoryDto>(category);
            }
            return createCategoryCommandResponse;
        }
    }
}
