using FastFood.Application.Common;
using FastFood.Application.Features.Categories;
using FastFood.Application.Features.Categories.Commands;
using FastFood.Application.Features.Categories.Queries;
using FastFood.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Presentation.Controllers;

[Route("/api/categories")]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PagedParameters pagedParameters)
    {
        var categories = await _mediator.Send(new GetCategoryQuery(pagedParameters));

        return Ok(new PagedResponse<CategoryDto>(categories));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var category = await _mediator.Send(command);

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}