using System;
using Common.Logging;

namespace LightDataInterface.Core
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        private readonly ILog _log;
        private readonly long _txId;

        #region Implementation of IUnitOfWork

        public bool AutoCommit { get; set; }
        public bool IsFinished { get; protected set; }
        public IDataSession DataSession { get; }

        #endregion

        protected BaseUnitOfWork(ILog log, IDataSession dataSession)
        {
            _log = log;
            _txId = DateTime.UtcNow.Ticks;
            _log.Debug(x => x("Starting transaction {0}.", _txId));
            DataSession = dataSession;
            AutoCommit = dataSession.AutoCommit;
            IsFinished = false;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (!IsFinished)
            {
                if (AutoCommit)
                {
                    _log.Debug(x => x("Automatically callig Commit prior disposal."));
                    Commit();
                }
                else
                {
                    _log.Warn(x => x("Rolling back transaction {0} because there was no explicit commit or rollback.", _txId));
                    Rollback();
                }
            }
            OnDispose();            
        }

        #endregion

        #region Implementation of IUnitOfWork

        public void Commit()
        {
            _log.Debug(x => x("Commiting transaction {0}.", _txId));
            DataSession.Flush();
            if (IsFinished)
            {
                throw new DataAccessException("Attempted commit, but transaction was not started.");
            }
            OnCommit();
            IsFinished = true;
            _log.Debug(x => x("Transaction commited {0}.", _txId));
        }

        public void Rollback()
        {
            _log.Debug(x => x("Rolling back transaction {0}.", _txId));
            if (IsFinished)
            {
                throw new DataAccessException("Attempted rollback, but transaction was not started.");
            }
            OnRollback();
            IsFinished = true;
        }

        #endregion

        protected abstract void OnCommit();
        protected abstract void OnRollback();
        protected abstract void OnDispose();
    }
}