using System;

namespace LightDataInterface
{
    public class DataSession
    {
        private static Func<string, IDataSession> _provider;

        public static void SetProvider(Func<string, IDataSession> provider)
        {
            _provider = provider;
        } 


        public static IDataSession Current(string name = null) => _provider(name);
    }
}