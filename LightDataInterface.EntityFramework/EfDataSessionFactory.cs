using System;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public class EfDataSessionFactory : DataSessionFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSessionFactory>();

        protected Func<string, IDataSession> FactoryMethod { get; set; }

        public EfDataSessionFactory(Func<string, IDataSession> factoryMethod)
            : base(Log)
        {
            FactoryMethod = factoryMethod;
        }

        #region Overrides of DataSessionFactory

        protected override IDataSession CreateDataSessionInternal(string name)
        {
            var dataSession = FactoryMethod(name);
            return dataSession;
        }

        #endregion
    }
}