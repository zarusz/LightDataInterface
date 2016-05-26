using System;

namespace LightDataInterface
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        IUnitOfWork CreateUnitOfWork();
        IDataSession DataSession { get; }
        bool AutoCommit { get; set; }
    }
}