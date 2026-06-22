using BuildingBlocks.Exceptions;

namespace Catalog.API.Exception;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product", id.ToString())
    {
    }
}