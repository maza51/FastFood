using System.Text.RegularExpressions;
using FastFood.Application.Exceptions;
using FastFood.Application.Interfaces;
using FastFood.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFood.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly ImageOptions _imageOptions;
    private readonly string _baseDir;

    public ImageService(IOptions<ImageOptions> imageConfiguration)
    {
        _imageOptions = imageConfiguration.Value;
        _baseDir = AppDomain.CurrentDomain.BaseDirectory;
    }

    public async Task<bool> IsSameImageAsync(string pathSavedImage, string imageBase64)
    {
        var imagePath = Path.Combine(_baseDir, pathSavedImage);

        var imageBytes = await File.ReadAllBytesAsync(imagePath);
        
        var base64Saved = Convert.ToBase64String(imageBytes);
        
        var (_, base64) = ParseBase64Image(imageBase64);

        if (base64 == base64Saved)
        {
            return true;
        }

        return false;
    }

    public async Task<string> SaveAsync(string imageBase64)
    {
        var (extension, base64) = ParseBase64Image(imageBase64);

        ValidateExtension(extension, _imageOptions.AllowedExtensions);

        var imageDir = Path.Combine(_baseDir, _imageOptions.Folder);
        
        if (!Directory.Exists(imageDir))
            Directory.CreateDirectory(imageDir);

        string fileName;
        string filePath;
        
        do
        {
            fileName = GenerateFileName(extension);
            filePath = Path.Combine(imageDir, fileName);
        } while (File.Exists(filePath));

        var bytes = Convert.FromBase64String(base64);
        
        await File.WriteAllBytesAsync(filePath, bytes);
        
        return Path.Combine(_imageOptions.Folder, fileName);
    }

    private void ValidateExtension(string extension, IEnumerable<string> allowedExtensions)
    {
        if (allowedExtensions.All(ext => ext != extension.ToLower()))
        {
            throw new BadRequestException("Incorrect image extension");
        }
    }

    private string GenerateFileName(string extension)
    {
        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        
        return $"{fileName}.{extension}";
    }

    private (string extension, string data) ParseBase64Image(string imageBase64)
    {
        var regex = new Regex("data:image/(?<extension>[^;]+);base64,(?<data>.+)");
        var match = regex.Match(imageBase64);
        
        if (!match.Success)
        {
            throw new BadRequestException("Incorrect image");
        }
        
        return (match.Groups["extension"].Value, match.Groups["data"].Value);
    }
}