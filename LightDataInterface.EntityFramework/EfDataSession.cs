using System.Data.Entity;
using Common.Logging;

namespace LightDataInterface.EntityFramework
{
    public class EfDataSession<T> : IDataSession
        where T : DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSession<T>>();

        protected internal string Name { get; private set; }
        protected internal T Db { get; private set; }
        public bool AutoFlush { get; set; }

        public EfDataSession(T db, string name)
        {
            Db = db;
            Name = name;
            AutoFlush = true;
            AutoCommit = true;
            // Associate the created context with the current thread.
            //ThreadDataContextHolder.SetByName(name, this);
        }

        public void Flush()
        {
            Log.Debug("Flushing changes to DB.");
            Db.SaveChanges();
        }

        public void Dispose()
        {
            if (Db != null)
            {
                if (AutoFlush)
                {
                    Log.Debug("Automatically calling Flush prior DataSession disposal.");
                    Flush();
                }
                Db.Dispose();
                Db = null;
            }
            // Clear this context from the current thread.
            //ThreadDataContextHolder.SetByName(Name, null);
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new EfUnitOfWork<T>(this);
        }

        public IDataSession DataSession => this;
        public bool AutoCommit { get; set; }

    }
}
