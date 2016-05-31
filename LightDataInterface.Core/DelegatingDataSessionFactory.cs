using System;
using System.Collections.Generic;
using Common.Logging;

namespace LightDataInterface.Core
{
    public class DelegatingDataSessionFactory : BaseDataSessionFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<DelegatingDataSessionFactory>();

        private readonly IDictionary<string, IDataSessionFactory> _dataSessionFactoryByName = new Dictionary<string, IDataSessionFactory>();

        public DelegatingDataSessionFactory()
            : base(Log)
        {
        }

        public void AddFactoryForName(string name, IDataSessionFactory factoryMethod)
        {
            _dataSessionFactoryByName.Add(name, factoryMethod);
        }

        #region Overrides of BaseDataSessionFactory

        protected override IDataSession CreateDataSessionInternal(string name)
        {
            IDataSessionFactory dataSessionFactoryForName;
            if (_dataSessionFactoryByName.TryGetValue(name, out dataSessionFactoryForName))
            {
                var dataSession = dataSessionFactoryForName.CreateDataSession(name);
                return dataSession;
            }
            return null;
        }

        protected override void OnDispose()
        {
            if (_dataSessionFactoryByName.Count > 0)
            {
                foreach (var dataSessionFactory in _dataSessionFactoryByName.Values)
                {
                    dataSessionFactory.Dispose();
                }
                _dataSessionFactoryByName.Clear();
            }
        }

        #endregion
    }
}
