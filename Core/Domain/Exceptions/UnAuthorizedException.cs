namespace Domain.Exceptions
{
    public sealed class UnAuthorizedException(string msg = "Invalid email or password") : Exception(msg)
    {
    }
}
