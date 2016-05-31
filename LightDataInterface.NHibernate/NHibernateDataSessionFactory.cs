using System.Collections.Generic;
using Common.Logging;
using LightDataInterface.Core;
using NHibernate;

namespace LightDataInterface.NHibernate
{
    public class NHibernateDataSessionFactory : BaseDataSessionFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<NHibernateDataSessionFactory>();
        private readonly IDictionary<string, ISessionFactory> _sessionFactoryByName = new Dictionary<string, ISessionFactory>();

        public NHibernateDataSessionFactory() 
            : base(Log)
        {
        }

        public void AddNamedSessionFactory(string name, ISessionFactory sessionFactory)
        {
            _sessionFactoryByName.Add(name, sessionFactory);
        }

        #region Overrides of BaseDataSessionFactory

        protected override IDataSession CreateDataSessionInternal(string name)
        {
            ISessionFactory sessionFactory;
            if (_sessionFactoryByName.TryGetValue(name, out sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                var dataSession = new NHibernateDataSession(name, session);
                return dataSession;
            }
            // When name not recognized return null
            return null;
        }

        #endregion

        #region Overrides of BaseDataSessionFactory

        protected override void OnDispose()
        {
            if (_sessionFactoryByName.Count > 0)
            {
                foreach (var sessionFactory in _sessionFactoryByName.Values)
                {
                    sessionFactory.Dispose();
                }
                _sessionFactoryByName.Clear();
            }
        }

        #endregion
    }
}