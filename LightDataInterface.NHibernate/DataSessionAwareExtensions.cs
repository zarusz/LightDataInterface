using LightDataInterface.Core;
using NHibernate;

namespace LightDataInterface.NHibernate
{
    public static class DataSessionAwareExtensions
    {
        /// <summary>
        /// Provides the NHibernate's <see cref="ISession"/> from the <see cref="IDataSessionAware"/>.
        /// </summary>
        /// <param name="dataSessionAware"></param>
        /// <returns></returns>
        public static ISession GetSession(this IDataSessionAware dataSessionAware)
        {
            var dataSession = dataSessionAware.GetDataSession();
            var dbContext = dataSession.GetSession();
            return dbContext;
        }
    }
}