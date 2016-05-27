using System.Data.Entity;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    public class EfDataSession<T> : BaseDataSession
        where T : DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSession<T>>();

        protected internal T Db { get; private set; }

        public EfDataSession(T db, string name)
            : base(Log, name)
        {
            Db = db;
        }

        protected override void OnFlush()
        {
            Db.SaveChanges();
        }

        protected override void OnDispose()
        {
            if (Db != null)
            {
                Db.Dispose();
                Db = null;
            }
        }

        protected override IUnitOfWork CreateUnitOfWorkInternal()
        {
            var unitOfWork = new EfUnitOfWork<T>(this);
            return unitOfWork;
        }
    }
}
