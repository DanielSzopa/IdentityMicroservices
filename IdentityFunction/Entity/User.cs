using IdentityFunction.ValueObjects;

namespace IdentityFunction.Entity;

internal class User
{
    internal FirstName FirstName { get; private set; }
    internal LastName LastName { get; private set; }
    internal Email Email { get; private set; }

    private User(FirstName firstName, LastName lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    internal static User Create(FirstName firstName, LastName lastName, Email email)
        => new (firstName, lastName, email);
}
