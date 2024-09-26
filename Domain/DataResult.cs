namespace Domain;

public class DataResult<T>
{
    public T? Value { get; set; }
    private bool Success { get; set; }
    public string? ErrorMessage { get; }

    public bool Failed()
    {
        return !Success;
    }

    public DataResult(T value)
    {
        Value = value;
        Success = true;
        ErrorMessage = null;
    }
    
    public DataResult(string errorMessage)
    {
        Value = default;
        Success = false;
        ErrorMessage = errorMessage;
    }
}