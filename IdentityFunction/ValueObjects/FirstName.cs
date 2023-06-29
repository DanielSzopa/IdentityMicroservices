using IdentityFunction.Exceptions;

namespace IdentityFunction.ValueObjects;

internal record FirstName
{
    internal string Value { get; }

    internal FirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName)) throw new InvalidFirstNameException(firstName);

        Value = firstName;
    }

    public static implicit operator FirstName(string firstName) => new (firstName);

    public static implicit operator string(FirstName firstName) => firstName.Value;
}
