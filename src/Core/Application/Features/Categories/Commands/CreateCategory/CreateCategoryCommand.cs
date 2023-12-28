using Application.Interfaces;
using AutoMapper;
using MediatR;
using Domain.Entities;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand : IRequest<Guid>
    {
        public string CategoryName { get; set; } = string.Empty;
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepository;
        public CreateCategoryCommandHandler(IMapper mapper,
                                          IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);

            var validator = new CreateCategoryCommandValidator();
            var result = await validator.ValidateAsync(request);
            if(result.Errors.Any())
            {
                throw new Exception("Category is not valid");
            }

            category = await _categoryRepository.AddAsync(category);
            return category.Id;
        }
    }

}
