namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"UpdateProductCommandHandler.Handle called with {request}");

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        product.Update(request.Name, request.Description, request.Price, request.Category, request.ImageFile);
        
        session.Update(product);
        
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}