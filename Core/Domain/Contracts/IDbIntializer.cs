namespace Domain.Contracts
{
    public interface IDbIntializer
    {
        Task IntializAsync();
        Task IntializIdentityAsync();
    }
}
