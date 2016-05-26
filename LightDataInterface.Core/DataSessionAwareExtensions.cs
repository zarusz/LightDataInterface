namespace LightDataInterface.Core
{
    public static class DataSessionAwareExtensions
    {
        public static IDataSession DataSession(this IDataSessionAware dataSessionAware)
        {
            var dataSession = LightDataInterface.DataSession.Current(dataSessionAware.DataName);
            return dataSession;
        }
    }
}