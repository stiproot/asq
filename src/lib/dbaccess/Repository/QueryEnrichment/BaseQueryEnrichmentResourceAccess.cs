using Microsoft.EntityFrameworkCore;
using dbaccess.Models;
using dbaccess.Common;

namespace dbaccess.Repository.QueryEnrichment
{
    public abstract class BaseQueryEnrichmentResourceAccess
    {
        protected readonly ASQContext _context;

        public BaseQueryEnrichmentResourceAccess(
            IAsqDbContextFactory<ASQContext> contextFactory
        )
        {
            this._context = contextFactory.CreateContext();
            this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}