using System;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Logging;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    /// <summary>
    /// <see cref="IDataSessionFactory"/> adapter for Entity Framework <see cref="DbContext"/> creation.
    /// </summary>
    public class EfDataSessionFactory : BaseDataSessionFactory
    {
        private static readonly ILog Log = LogManager.GetLogger<EfDataSessionFactory>();
        private readonly IDictionary<string, Func<IDataSession>> _factoryMethodByName = new Dictionary<string, Func<IDataSession>>();

        public EfDataSessionFactory() 
            : base(Log)
        {
        }

        public void AddFactoryMethodForName(string name, Func<IDataSession> factoryMethod)
        {
            _factoryMethodByName.Add(name, factoryMethod);
        }

        #region Overrides of BaseDataSessionFactory

        protected override IDataSession CreateDataSessionInternal(string name)
        {
            Func<IDataSession> factoryMethod;
            if (_factoryMethodByName.TryGetValue(name, out factoryMethod))
            {
                var dataSession = factoryMethod();
                return dataSession;
            }
            // When name not recognized return null
            return null;
        }

        #endregion

        #region Overrides of BaseDataSessionFactory

        protected override void OnDispose()
        {
        }

        #endregion
    }
}