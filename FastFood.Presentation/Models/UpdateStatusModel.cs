using FastFood.Domain.Enums;

namespace FastFood.Presentation.Models;

public class UpdateStatusModel
{
    public OrderStatus Status { get; set; }
}