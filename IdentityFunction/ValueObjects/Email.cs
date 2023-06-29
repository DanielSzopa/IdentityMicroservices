using IdentityFunction.Exceptions;

namespace IdentityFunction.ValueObjects;

internal record Email
{
    internal string Value { get; }

    internal Email(string email)
    {
        if (string.IsNullOrEmpty(email)) throw new InvalidEmailException(email);

        Value = email;
    }

    public static implicit operator Email(string email) => new (email);

    public static implicit operator string(Email email) => email.Value;
}
