using System;

namespace LightDataInterface
{
    public interface IDataSessionFactory : IDisposable
    {
        IDataSession CreateDataSession(string name);
    }
}
