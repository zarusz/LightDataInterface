using System;

namespace LightDataInterface
{
    public class DataSession
    {
        private static Func<IDataSession> _provider;

        public static void SetProvider(Func<IDataSession> provider)
        {
            _provider = provider;
        } 


        public static IDataSession Current => _provider();
    }
}