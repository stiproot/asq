using dbaccess.Models;
using Microsoft.EntityFrameworkCore;

namespace dbaccess.Common
{
    public class AsqDbContextFactory : IAsqDbContextFactory<ASQContext>
    {
        private IConnectionStringManager _connectionStringManager;

        public AsqDbContextFactory(IConnectionStringManager connectionStringManager) 
            => _connectionStringManager = connectionStringManager;

        public ASQContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ASQContext>();

            optionsBuilder.UseMySQL(_connectionStringManager.GetConnectionString());

            return new ASQContext(optionsBuilder.Options);
        }
    }
}