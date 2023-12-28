using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CreateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepository;
        public CreateCategoryCommandHandler(IMapper mapper,
                                          IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var isExists = _categoryRepository.GetByIdAsync(category.Id);
            if (isExists == null)
                throw new Exception("Not Found");

            await _categoryRepository.UpdateAsync(category);

            return Unit.Value;
        }
    }
}
