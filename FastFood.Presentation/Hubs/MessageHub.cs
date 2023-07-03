using Microsoft.AspNetCore.SignalR;

namespace FastFood.Presentation.Hubs;

public interface IMessageHub
{
    Task OrdersChanged();
}

public class MessageHub : Hub<IMessageHub>{}