using Common.Logging;

namespace LightDataInterface.Core
{
    public abstract class DataSessionFactory : IDataSessionFactory
    {
        private readonly ILog _log;

        protected DataSessionFactory(ILog log)
        {
            _log = log;
            _log.Debug("Creation.");
            //ThreadDataContextHolder.DefaultName = DefaultName;
        }

        #region Implementation of IDisposable

        public virtual void Dispose()
        {
            _log.Debug("Disposing.");
        }

        protected abstract IDataSession CreateDataSessionInternal(string name);

        public IDataSession CreateDataSession(string name)
        {
            _log.Debug(x => x("Creating DataSession named {0}.", name));
            var dataContext = CreateDataSessionInternal(name);
            if (dataContext == null)
            {
                throw new DataAccessException($"The DataSession name {name} is not recognized. Make sure you configured the DataAccess later properly.");
            }

            return dataContext;
        }

        #endregion
    }
}