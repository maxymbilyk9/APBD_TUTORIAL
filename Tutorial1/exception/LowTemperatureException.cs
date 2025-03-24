namespace Tutorial1.exception;

public class LowTemperatureException : Exception
{
    
    public LowTemperatureException()
    {
    }

    public LowTemperatureException(string? message) : base(message)
    {
    }
    
}