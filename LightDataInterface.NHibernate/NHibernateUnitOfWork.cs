using Common.Logging;
using LightDataInterface.Core;
using NHibernate;

namespace LightDataInterface.NHibernate
{
    public class NHibernateUnitOfWork : BaseUnitOfWork
    {
        private static readonly ILog Log = LogManager.GetLogger<NHibernateUnitOfWork>();

        private ITransaction _tx;

        public NHibernateUnitOfWork(NHibernateDataSession dataSession)
            : base(Log, dataSession)
        {
            _tx = dataSession.Session.BeginTransaction();
        }

        #region Overrides of BaseUnitOfWork

        protected override void OnCommit()
        {
            _tx.Commit();
        }

        protected override void OnRollback()
        {
            _tx.Rollback();
        }

        protected override void OnDispose()
        {
            if (_tx != null)
            {
                _tx.Dispose();
                _tx = null;
            }
        }

        #endregion
    }
}