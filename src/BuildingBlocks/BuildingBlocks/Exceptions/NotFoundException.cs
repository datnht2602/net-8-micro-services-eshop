namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }

    public NotFoundException(string name, string message) : base($"{name} not found: {message}")
    {
        
    }
}