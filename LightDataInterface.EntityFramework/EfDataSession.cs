﻿using System.Data.Entity;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    /// <summary>
    /// <see cref="IDataSession"/> adapter for Entity Framework's <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfDataSession<T> : BaseDataSession
        where T : DbContext
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ILog Log = LogManager.GetLogger(typeof(EfDataSession<>));

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
