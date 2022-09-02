using dbaccess.Common;
using dbaccess.Models;
using dbaccess.Repository;

namespace dbaccess.Factory
{
    public class GenericRespositoryFactory: IGenericRepositoryFactory
    {
        private readonly IAsqDbContextFactory<ASQContext> _contextFactory;

        public GenericRespositoryFactory(IAsqDbContextFactory<ASQContext> contextFactory)
            => this._contextFactory = contextFactory;

        public IGenericRepository<T> Create<T>() where T: class
            => new GenericRespository<T>(this._contextFactory);
    }
}