namespace IdentityFunction.Exceptions;

internal class InvalidLastNameException : CustomException
{
    internal InvalidLastNameException(string lastName) : base($"Invalid lastName: {lastName}")
    {
    }
}
