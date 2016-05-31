using NHibernate;

namespace LightDataInterface.NHibernate
{
    public static class DataSessionExtensions
    {
        /// <summary>
        /// Provides the NHibernate's <see cref="ISession"/> from the <see cref="IDataSession"/>.
        /// </summary>
        /// <param name="dataSession"></param>
        /// <returns></returns>
        public static ISession GetSession(this IDataSession dataSession)
        {
            var dbContext = ((NHibernateDataSession) dataSession).Session;
            return dbContext;
        }
    }
}