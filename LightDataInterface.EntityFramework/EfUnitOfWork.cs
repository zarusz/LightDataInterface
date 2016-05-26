using System;
using System.Data.Entity;
using Common.Logging;

namespace LightDataInterface.EntityFramework
{
    public class EfUnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSession<T>>();

        private readonly EfDataSession<T> _dataSession;
        private readonly long _txId;
        private DbContextTransaction _tx;
        public bool AutoCommit { get; set; }

        public EfUnitOfWork(EfDataSession<T> dataSession)
        {
            _dataSession = dataSession;
            _txId = DateTime.UtcNow.Ticks;
            // generate a transaction id for logging purpouse
            Log.Debug(x => x("Starting transaction {0}.", _txId));
            _tx = dataSession.Db.Database.BeginTransaction();
            IsFinished = false;
            AutoCommit = dataSession.AutoCommit;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (!IsFinished)
            {
                if (AutoCommit)
                {
                    Log.Debug(x => x("Automatically callig Commit prior disposal."));
                    Commit();
                }
                else
                {
                    Log.Warn(x => x("Rolling back transaction {0} because there was no explicit commit or rollback.", _txId));
                    Rollback();
                }
            }
            if (_tx != null)
            {
                _tx.Dispose();
                _tx = null;
            }
        }

        #endregion

        #region Implementation of IUnitOfWork

        public IDataSession DataSession => _dataSession;
        public bool IsFinished { get; protected set; }

        public void Commit()
        {
            Log.Debug(x => x("Commiting transaction {0}.", _txId));
            _dataSession.Flush();
            if (IsFinished)
            {
                throw new DataAccessException("Attempted commit, but transaction was not started.");
            }
            _tx.Commit();
            IsFinished = true;
            Log.Debug(x => x("Transaction commited {0}.", _txId));
        }

        public void Rollback()
        {
            Log.Debug(x => x("Rolling back transaction {0}.", _txId));
            if (IsFinished)
            {
                throw new DataAccessException("Attempted rollback, but transaction was not started.");
            }
            _tx.Rollback();
            IsFinished = true;
        }

        #endregion
    }
}