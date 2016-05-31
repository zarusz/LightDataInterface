using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public static class DataSessionAwareExtensions
    {
        /// <summary>
        /// Provides the Entity Framework's <see cref="DbContext"/> from the <see cref="IDataSessionAware"/>.
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="dataSessionAware"></param>
        /// <returns></returns>
        public static TDbContext GetDbContext<TDbContext>(this IDataSessionAware dataSessionAware)
            where TDbContext : DbContext
        {
            var dataSession = dataSessionAware.GetDataSession();
            var dbContext = dataSession.GetDbContext<TDbContext>();
            return dbContext;
        }
    }
}