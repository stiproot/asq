using dbaccess.Repository;

namespace dbaccess.Factory
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<T> Create<T>() where T: class;
    }
}