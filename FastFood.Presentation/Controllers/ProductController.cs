using FastFood.Application.Common;
using FastFood.Application.Features.Products;
using FastFood.Application.Features.Products.Commands;
using FastFood.Application.Features.Products.Queries;
using FastFood.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Presentation.Controllers;

[Route("/api/products")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedList([FromQuery] PagedParameters pagedParameters)
    {
        var products = await _mediator.Send(new GetProductQuery(pagedParameters));
        
        return Ok(new PagedResponse<ProductDto>(products));
    }

    [HttpGet("new")]
    public async Task<IActionResult> GetNews([FromQuery] PagedParameters pagedParameters)
    {
        var products = await _mediator.Send(new GetProductNewQuery(pagedParameters));
        
        return Ok(new PagedResponse<ProductDto>(products));
    }

    [HttpGet("/api/categories/{categoryId:int}/products")]
    public async Task<IActionResult> GetByCategoryId([FromRoute] int categoryId, [FromQuery] PagedParameters pagedParameters)
    {
        var products = await _mediator.Send(new GetProductByCategoryIdQuery(categoryId, pagedParameters));

        return Ok(new PagedResponse<ProductDto>(products));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));

        return Ok(product);
    }

    [HttpPost("/api/categories/{categoryId:int}/products")]
    public async Task<IActionResult> Create([FromRoute] int categoryId, [FromBody] CreateProductCommand command)
    {
        command.CategoryId = categoryId;
        
        var product = await _mediator.Send(command);

        return Ok(product);
    }

    [HttpPut("/api/categories/{categoryId:int}/products")]
    public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] UpdateProductCommand command)
    {
        command.CategoryId = categoryId;

        var product = await _mediator.Send(command);

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}