using FastFood.Presentation.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FastFood.Presentation.Services;

public interface INotificationService
{
    Task OrdersChanged();
}

public class HubNotificationService : INotificationService
{
    private readonly IHubContext<MessageHub, IMessageHub> _hubContext;

    public HubNotificationService(IHubContext<MessageHub, IMessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task OrdersChanged()
    {
        await _hubContext.Clients.All.OrdersChanged();
    }
}