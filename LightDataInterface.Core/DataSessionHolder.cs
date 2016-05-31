using System;
using System.Collections.Generic;
using Common.Logging;

namespace LightDataInterface.Core
{
    /// <summary>
    /// Simple holder to store all the active named <see cref="IDataSession"/>-s.
    /// The object is meant to be bound to a thread or a web-request.
    /// This class is NOT thread-safe.
    /// </summary>
    public class DataSessionHolder : IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger<DataSessionHolder>();

        private readonly IDataSessionFactory _dataSessionFactory;
        private readonly IDictionary<string, IDataSession> _dataSessionByName = new Dictionary<string, IDataSession>();

        public string DefaultName { get; set; }

        public DataSessionHolder(IDataSessionFactory dataSessionFactory)
        {
            Log.Debug("Holder created.");
            _dataSessionFactory = dataSessionFactory;
            DefaultName = "default";
        }

        public IDataSession GetByName(string name)
        {
            if (name == null)
            {
                name = DefaultName;
            }

            IDataSession ctx;
            if (_dataSessionByName.TryGetValue(name, out ctx))
            {
                return ctx;
            }

            Log.Debug(x => x("Lazy creaton of data session with name {0}.", name));
            ctx = _dataSessionFactory.CreateDataSession(name);
            SetByName(name, ctx);

            return ctx;
        }

        /// <summary>
        /// Adds a named <see cref="IDataSession"/> to the holder.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        public void SetByName(string name, IDataSession context)
        {
            _dataSessionByName[name] = context;
            Log.Debug(x => x("Adding data session of name {0} into holder.", name));
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Disposes all <see cref="IDataSession"/> it stores.
        /// </summary>
        public void Dispose()
        {
            if (_dataSessionByName.Count > 0)
            {
                Log.Debug(x => x("Disposing {0} data sessions.", _dataSessionByName.Count));
                foreach (var dataSession in _dataSessionByName.Values)
                {
                    dataSession.Dispose();
                }
                _dataSessionByName.Clear();                
            }
            Log.Debug("Holder disposed.");
        }

        #endregion
    }
}