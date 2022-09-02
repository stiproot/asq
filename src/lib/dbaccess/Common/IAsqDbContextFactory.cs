using Microsoft.EntityFrameworkCore;

namespace dbaccess.Common
{
    public interface IAsqDbContextFactory<T> where T : DbContext
    {
        T CreateContext();
    }
}