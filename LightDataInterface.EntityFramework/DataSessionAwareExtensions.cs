using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public static class DataSessionAwareExtensions
    {
        public static TDbContext DbContext<TDbContext>(this IDataSessionAware dataSessionAware)
            where TDbContext : DbContext
        {
            var dataSession = dataSessionAware.DataSession();
            var dbContext = dataSession.DbContext<TDbContext>();
            return dbContext;
        }
    }
}