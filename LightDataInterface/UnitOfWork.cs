using System;

namespace LightDataInterface
{
    public class UnitOfWork
    {
        private static Func<IUnitOfWork> _provider;

        public static void SetProvider(Func<IUnitOfWork> provider)
        {
            _provider = provider;
        }


        public static IUnitOfWork Current => _provider();
    }
}