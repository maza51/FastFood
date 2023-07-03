using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;

namespace FastFood.Application.Services;

public class BuyerCodeService : IBuyerCodeService
{
    private readonly IUnitOfWork _unitOfWork;

    public BuyerCodeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateCodeAsync()
    {
        var code = 1;
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        var ordersToday = await _unitOfWork.Orders.GetByDateAsync(today);

        Order? lastOrder;
        
        if (ordersToday.Any())
        {
            lastOrder = ordersToday.MaxBy(x => x.BuyerCode);
        }
        else
        {
            var activeOrders = await _unitOfWork.Orders.GetActive();
            
            lastOrder = activeOrders.MaxBy(x => x.BuyerCode);
        }
        
        if (lastOrder != null)
        {
            code = int.Parse(lastOrder.BuyerCode);
            code++;
        }

        return code.ToString().PadLeft(4, '0');
    }
}