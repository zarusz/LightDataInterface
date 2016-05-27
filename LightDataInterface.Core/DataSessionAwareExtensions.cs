namespace LightDataInterface.Core
{
    public static class DataSessionAwareExtensions
    {
        public static IDataSession GetDataSession(this IDataSessionAware dataSessionAware)
        {
            var dataSession = DataSession.Current(dataSessionAware.DataName);
            return dataSession;
        }
    }
}