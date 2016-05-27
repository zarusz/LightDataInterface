using System.Data.Entity;

namespace LightDataInterface.EntityFramework
{
    public static class DataSessionExtensions
    {
        public static TDbContext DbContext<TDbContext>(this IDataSession dataSession)
            where TDbContext : DbContext
        {
            var dbContext = ((EfDataSession<TDbContext>) dataSession).Db;
            return dbContext;
        }
    }
}