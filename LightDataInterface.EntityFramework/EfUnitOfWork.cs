using System.Data.Entity;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public class EfUnitOfWork<T> : BaseUnitOfWork
        where T : DbContext
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ILog Log = LogManager.GetLogger(typeof(EfUnitOfWork<>));

        private DbContextTransaction _tx;

        public EfUnitOfWork(EfDataSession<T> dataSession)
            : base(Log, dataSession)
        {
            _tx = dataSession.Db.Database.BeginTransaction();
        }

        protected override void OnDispose()
        {
            if (_tx != null)
            {
                _tx.Dispose();
                _tx = null;
            }
        }

        #region Implementation of IUnitOfWork

        protected override void OnCommit()
        {
            _tx.Commit();
        }

        protected override void OnRollback()
        {
            _tx.Rollback();
        }

        #endregion
    }
}