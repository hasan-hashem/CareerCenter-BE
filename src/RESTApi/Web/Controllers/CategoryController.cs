using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetCategories;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name ="Get")]
        public async Task<ActionResult<List<CategoryVm>>> Get()
        {
            var result =  await _mediator.Send(new GetCategoriesQuery());
            return result == null ? NotFound(): Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryCommand command)
        {
            Guid Id = await _mediator.Send(command);
            return Ok(Id);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            return result == null ? NotFound() : NoContent();
        }
    }
}
