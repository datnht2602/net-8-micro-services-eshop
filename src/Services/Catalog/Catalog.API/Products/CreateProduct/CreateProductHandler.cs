namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price, List<string> Category, string ImageFile) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            ImageFile = request.ImageFile
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}