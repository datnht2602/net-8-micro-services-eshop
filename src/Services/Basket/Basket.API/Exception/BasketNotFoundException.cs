namespace Basket.API.Exception;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base("Basket", userName)
    {
    }

    public BasketNotFoundException(string name, string message) : base(name, message)
    {
    }
}