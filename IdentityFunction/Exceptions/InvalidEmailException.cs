namespace IdentityFunction.Exceptions;

internal class InvalidEmailException : CustomException
{
    internal InvalidEmailException(string email) : base($"Invalid email: {email}")
    {
    }
}
