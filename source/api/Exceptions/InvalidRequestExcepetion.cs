using FluentValidation.Results;
using System.Runtime.Serialization;

namespace connect_s_event_api.Exceptions;
[Serializable]
internal class InvalidRequestExcepetion : Exception
{
    public IEnumerable<InvalidRequestError> Errors;

    public InvalidRequestExcepetion()
    {
        Errors = new List<InvalidRequestError>();
    }

    public InvalidRequestExcepetion(List<ValidationFailure> errors)
    {
        Errors = errors.Select(_ => new InvalidRequestError(_.PropertyName, _.ErrorMessage));
    }

    public InvalidRequestExcepetion(string? message) : base(message)
    {
        Errors = new List<InvalidRequestError>();
    }

    public InvalidRequestExcepetion(string? message, Exception? innerException) : base(message, innerException)
    {
        Errors = new List<InvalidRequestError>();
    }

    protected InvalidRequestExcepetion(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Errors = new List<InvalidRequestError>();
    }
}
public record InvalidRequestError(string PropertyName, string ErrorMessage);
