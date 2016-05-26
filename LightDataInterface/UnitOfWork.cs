using System;

namespace LightDataInterface
{
    public class UnitOfWork
    {
        private static Func<string, IUnitOfWork> _provider;

        public static void SetProvider(Func<string, IUnitOfWork> provider)
        {
            _provider = provider;
        }


        public static IUnitOfWork Current(string name) => _provider(name);
    }
}