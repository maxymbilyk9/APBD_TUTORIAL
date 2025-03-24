namespace Tutorial1.exception;

public class ContainerNotFoundException : Exception
{
    
    public ContainerNotFoundException()
    {
    }

    public ContainerNotFoundException(string? message) : base(message)
    {
    }
}