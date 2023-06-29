namespace IdentityFunction.Requests;

internal record CreateUser(string FirstName, string LastName, string Email, bool IsNewsletterSubscriber);
