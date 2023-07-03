namespace FastFood.Application.Interfaces;

public interface IBuyerCodeService
{
    Task<string> GenerateCodeAsync();
}