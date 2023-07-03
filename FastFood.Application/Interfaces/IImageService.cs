namespace FastFood.Application.Interfaces;

public interface IImageService
{
    Task<bool> IsSameImageAsync(string pathSavedImage, string imageBase64);
    Task<string> SaveAsync(string imageBase64);
}