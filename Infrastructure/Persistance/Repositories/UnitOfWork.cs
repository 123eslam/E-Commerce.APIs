namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        //private Dictionary<string, object> _repository;
        private ConcurrentDictionary<string, object> _repository;
        public UnitOfWork(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
            _repository = new();
        }
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //return new GenericRepository<TEntity, Tkey>(_dbContext);
            //Dictionary
            //Repositories
            //Key      : Value
            //Product  : new GenericRepository<TEntity, Tkey>(_dbContext);

            //var typeName = typeof(TEntity).Name;
            //if (_repository.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, Tkey>) _repository[typeName];
            //else
            //{
            //    var repo = new GenericRepository<TEntity, Tkey>(_dbContext);
            //    _repository.Add(typeName, repo);
            //    return repo;
            //}

            return (IGenericRepository<TEntity, Tkey>) _repository.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, Tkey>(_dbContext));
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
