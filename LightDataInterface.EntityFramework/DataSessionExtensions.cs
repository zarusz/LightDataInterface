using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public static class DataSessionExtensions
    {
        /// <summary>
        /// Provides the Entity Framework's <see cref="DbContext"/> from the <see cref="IDataSession"/>.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="dataSession"></param>
        /// <returns></returns>
        public static TDbContext GetDbContext<TDbContext>(this IDataSession dataSession)
            where TDbContext : DbContext
        {
            var dbContext = ((EfDataSession<TDbContext>) dataSession).Db;
            return dbContext;
        }
    }
}