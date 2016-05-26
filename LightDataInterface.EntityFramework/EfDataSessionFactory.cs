using System;
using Common.Logging;

namespace LightDataInterface.EntityFramework
{
    public class EfDataSessionFactory : IDataSessionFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSessionFactory>();

        public string DefaultContextName { get; protected set; }
        public Func<string, IDataSession> FactoryMethod { get; protected set; }

        public EfDataSessionFactory(string defaultContextName, Func<string, IDataSession> factoryMethod)
        {
            Log.Debug("Creation.");
            DefaultContextName = defaultContextName;
            FactoryMethod = factoryMethod;

            //ThreadDataContextHolder.DefaultContextName = defaultContextName;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Log.Debug("Disposing.");
        }

        public virtual IDataSession CreateDataSession(string name = null)
        {
            if (name == null)
            {
                name = DefaultContextName;
            }

            Log.Debug(x => x("Creating DataSession named {0}.", name));
            var dataContext = FactoryMethod(name);
            if (dataContext == null)
            {
                throw new DataAccessException($"The DataSession name {name} is not recognized. Make sure you configured the DataAccess later properly.");
            }

            return dataContext;
        }

        #endregion
    }
}