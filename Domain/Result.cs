namespace Domain;

public static class Result
{
    public static DataResult<T> Success<T>(T value)
    {
        return new DataResult<T>(value);
    }
    
    public static DataResult<T> Failure<T>(string errorMessage)
    {
        return new DataResult<T>(errorMessage);
    }
}
