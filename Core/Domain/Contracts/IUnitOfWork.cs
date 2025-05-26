using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        //SaveChanges
        Task<int> SaveChangesAsync();
        //Signature for function will return an instance of class that implement IGenericRepository
        // new GenericRepository<Product, int>
        // new GenericRepository<ProductBrand, int>
        // new GenericRepository<ProductType, int>
        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
