using IdentityFunction.Exceptions;

namespace IdentityFunction.ValueObjects;

internal record LastName
{
    internal string Value { get; }

    internal LastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName)) throw new InvalidLastNameException(lastName);

        Value = lastName;
    }

    public static implicit operator LastName(string lastName) => new (lastName);

    public static implicit operator string(LastName lastName) => lastName.Value;
}
