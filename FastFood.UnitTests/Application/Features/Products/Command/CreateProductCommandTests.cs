using System.Reflection;
using AutoMapper;
using FastFood.Application.Features.Products;
using FastFood.Application.Features.Products.Commands;
using FastFood.Application.Interfaces;
using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities;
using Moq;

namespace FastFood.UnitTests.Application.Features.Products.Command;

public class CreateProductCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandTests()
    {
        var productRepository = new Mock<IProductRepository>();

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _unitOfWorkMock
            .Setup(x => x.Products)
            .Returns(productRepository.Object);

        var mapper = new MapperConfiguration(cfg => 
            cfg.AddMaps(Assembly.Load("FastFood.Application"))).CreateMapper();

        var imageServiceMock = new Mock<IImageService>();

        imageServiceMock.Setup(x => x.SaveAsync(It.IsAny<string>()))
            .Returns(Task.FromResult("Images/test.jpeg"));
        
        _handler = new CreateProductCommandHandler(_unitOfWorkMock.Object, mapper, imageServiceMock.Object);
    }

    [Fact]
    public async Task ReturnProductDto()
    {
        var command = new CreateProductCommand
        {
            Name = "name",
            Description = "wtf",
            UnitPrice = 1.01m,
            CategoryId = 1,
            ImageBase64 = "eqwr"
        };
        
        var result = await _handler.Handle(command, CancellationToken.None);

        _unitOfWorkMock.Verify(uow => uow.Products.Create(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangeAsync(), Times.Once);
        Assert.True(result.GetType() == typeof(ProductDto));
    }
}