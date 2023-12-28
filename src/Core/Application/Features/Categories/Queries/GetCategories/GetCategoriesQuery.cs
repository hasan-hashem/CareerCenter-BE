using MediatR;

namespace Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<CategoryVm>>
    {
    }
}
