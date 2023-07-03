namespace FastFood.Application.Exceptions;

public class ValidationException : ApplicationException
{
    // public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : base("Validation Failure", "One or more validation errors occurred")
    // {
    //     ErrorsDictionary = errorsDictionary;
    // }
    //
    // public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    
    public ValidationException(IEnumerable<string> errors) : base("Validation Failure", "One or more validation errors occurred")
    {
        Errors = errors;
    }


    public IEnumerable<string> Errors { get; }
}