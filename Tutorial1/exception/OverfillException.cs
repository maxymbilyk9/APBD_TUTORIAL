namespace Tutorial1.exception;

public 
    class OverfillException : Exception
{
    public OverfillException()
    {
    }

    public OverfillException(string? message) : base(message)
    {
    }
    
}