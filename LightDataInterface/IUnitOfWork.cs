using System;

namespace LightDataInterface
{
    public interface IUnitOfWork : IDisposable
    {
        IDataSession DataSession { get; }
        bool AutoCommit { get; set; }
        bool IsFinished { get; }
        void Commit();
        void Rollback();
    }
}