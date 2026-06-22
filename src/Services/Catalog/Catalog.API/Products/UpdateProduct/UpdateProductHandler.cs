namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        product.Update(request.Name, request.Description, request.Price, request.Category, request.ImageFile);
        
        session.Update(product);
        
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}