using Common.Logging;

namespace LightDataInterface.Core
{
    public abstract class BaseDataSessionFactory : IDataSessionFactory
    {
        private readonly ILog _log;

        protected BaseDataSessionFactory(ILog log)
        {
            _log = log;
            _log.Debug("Creation.");
        }

        #region Implementation of IDisposable

        public virtual void Dispose()
        {
            _log.Debug("Disposing.");
            OnDispose();
        }

        #endregion

        public IDataSession CreateDataSession(string name)
        {
            _log.Debug(x => x("Creating GetDataSession named {0}.", name));
            var dataContext = CreateDataSessionInternal(name);
            if (dataContext == null)
            {
                throw new DataAccessException($"The DataSession name {name} is not recognized. Make sure you configured the DataAccess later properly.");
            }

            return dataContext;
        }

        protected abstract IDataSession CreateDataSessionInternal(string name);

        protected abstract void OnDispose();
    }
}