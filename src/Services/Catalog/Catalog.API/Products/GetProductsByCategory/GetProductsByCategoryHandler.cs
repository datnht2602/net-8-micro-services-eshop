namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductByIdHandler.Handler called with {request}");

        var products = await session.Query<Product>().Where(p => p.Category.Contains(request.Category)).ToListAsync(token: cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}