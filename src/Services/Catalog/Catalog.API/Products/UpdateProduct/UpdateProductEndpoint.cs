namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile);
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResponse>();

            return response;
        }).WithName("UpdateProduct")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product")
        .WithDescription("Update product");
    }
}