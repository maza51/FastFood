namespace FastFood.Infrastructure.Options;

public class ImageOptions
{
    public string Folder { get; set; } = null!;
    public List<string> AllowedExtensions { get; set; } = new List<string>();
}