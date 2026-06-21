namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = null!;
    public string ImageFile { get; set; } = null!;
    public decimal Price { get; set; }
    public List<string> Category { get; set; } = [];

    public void Update(string requestName, string requestDescription, decimal requestPrice, List<string> requestCategory, string requestImageFile)
    {
        Name = requestName;
        Description = requestDescription;
        Price = requestPrice;
        Category = requestCategory;
        ImageFile = requestImageFile;
    }
}