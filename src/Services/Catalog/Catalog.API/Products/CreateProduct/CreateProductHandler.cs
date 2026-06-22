namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price, List<string> Category, string ImageFile) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

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