using Common.Logging;
using LightDataInterface.Core;
using NHibernate;

namespace LightDataInterface.NHibernate
{
    public class NHibernateDataSession : BaseDataSession
    {
        private static readonly ILog Log = LogManager.GetLogger<NHibernateDataSession>();

        protected internal ISession Session;

        public NHibernateDataSession(string name, ISession session) 
            : base(Log, name)
        {
            Session = session;
        }

        #region Overrides of BaseDataSession

        protected override IUnitOfWork CreateUnitOfWorkInternal()
        {
            return new NHibernateUnitOfWork(this);
        }

        protected override void OnFlush()
        {
            Session.Flush();
        }

        protected override void OnDispose()
        {
            if (Session != null)
            {
                Session.Dispose();
                Session = null;
            }
        }

        #endregion
    }
}
