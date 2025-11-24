namespace TestScoring.Domain.Exceptions;

public class FileEmptyException : Exception
{
    public FileEmptyException(string? message) : base(message)
    {
    }
}