namespace LightDataInterface.Core
{
    public static class DataSessionAwareExtensions
    {
        /// <summary>
        /// Retrieve the <see cref="IDataSession"/> from the <see cref="IDataSessionAware"/>.
        /// </summary>
        /// <param name="dataSessionAware"></param>
        /// <returns></returns>
        public static IDataSession GetDataSession(this IDataSessionAware dataSessionAware)
        {
            var dataSession = DataSession.Current(dataSessionAware.DataName);
            return dataSession;
        }
    }
}