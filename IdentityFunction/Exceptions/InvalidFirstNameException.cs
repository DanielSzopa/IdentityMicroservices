namespace IdentityFunction.Exceptions;

internal class InvalidFirstNameException : CustomException
{
    internal InvalidFirstNameException(string firstName) : base($"Invalid firstName: {firstName}")
    {
    }
}
