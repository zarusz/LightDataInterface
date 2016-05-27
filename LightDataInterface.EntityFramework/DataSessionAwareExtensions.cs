using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public static class DataSessionAwareExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this IDataSessionAware dataSessionAware)
            where TDbContext : DbContext
        {
            var dataSession = dataSessionAware.GetDataSession();
            var dbContext = dataSession.DbContext<TDbContext>();
            return dbContext;
        }
    }
}