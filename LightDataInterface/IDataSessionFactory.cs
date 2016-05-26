using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightDataInterface
{
    public interface IDataSessionFactory : IDisposable
    {
        IDataSession CreateDataSession(string name = null);
    }
}
