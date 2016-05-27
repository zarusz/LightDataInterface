using System.Collections.Generic;
using System.Linq;
using Common.Logging;

namespace LightDataInterface.Core
{
    public abstract class BaseDataSession : IDataSession
    {
        private readonly ILog _log;
        private bool _isDisposed;
        public string Name { get; private set; }
        protected readonly ICollection<IUnitOfWork> ActiveUnitOfWorks = new HashSet<IUnitOfWork>();

        protected BaseDataSession(ILog log, string name)
        {
            _log = log;
            Name = name;
            AutoFlush = true;
            AutoCommit = true;
            _isDisposed = false;
        }

        public IDataSession DataSession => this;
        public bool AutoFlush { get; set; }
        public bool AutoCommit { get; set; }

        public void Flush()
        {
            _log.Debug("Flushing changes to DB.");
            OnFlush();
        }

        public virtual IUnitOfWork CreateUnitOfWork()
        {
            var unitOfWork = CreateUnitOfWorkInternal();
            ActiveUnitOfWorks.Add(unitOfWork);
            return unitOfWork;
        }

        protected abstract IUnitOfWork CreateUnitOfWorkInternal();

        protected internal void OnUnitOfWorkDisposed(IUnitOfWork unitOfWork)
        {
            ActiveUnitOfWorks.Remove(unitOfWork);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (!_isDisposed)
            {
                return;
            }

            if (AutoFlush)
            {
                _log.Debug("Automatically calling Flush prior GetDataSession disposal.");
                Flush();
            }

            if (ActiveUnitOfWorks.Count > 0)
            {
                _log.Warn(x => x("There were still {0} active UnitIfWork-s when your DataSession was disposed. Ensure you finish all your UnitOfWork-s before the owning DataSession gets disposed.", ActiveUnitOfWorks.Count));

                // dispose all transactions
                while (ActiveUnitOfWorks.Count > 0)
                {
                    ActiveUnitOfWorks.First().Dispose();
                }
            }

            OnDispose();
            _isDisposed = true;
        }

        #endregion

        protected abstract void OnFlush();
        protected abstract void OnDispose();
    }
}