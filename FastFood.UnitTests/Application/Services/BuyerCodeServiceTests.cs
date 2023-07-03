using FastFood.Application.Interfaces;
using FastFood.Application.Interfaces.Repository;
using FastFood.Application.Services;
using FastFood.Domain.Entities;
using Moq;

namespace FastFood.UnitTests.Application.Services;

public class BuyerCodeServiceTests
{
    [Fact]
    public async Task ReturnCode()
    {
        var ordersTodayList = new List<Order>
        {
            new Order
            {
                Id = 1,
                BuyerCode = "0001",
                CreatedAt = DateTime.Now
            },
            new Order
            {
                Id = 2,
                BuyerCode = "0002",
                CreatedAt = DateTime.Now
            }
        };
        
        var orderRepository = new Mock<IOrderRepository>();

        orderRepository.Setup(x => x.GetByDateAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(ordersTodayList);
        
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(x => x.Orders)
            .Returns(orderRepository.Object);

        var buyerCoderService = new BuyerCodeService(unitOfWork.Object);
        
        var code = await buyerCoderService.GenerateCodeAsync();
        
        Assert.True(code == "0003");
    }
}