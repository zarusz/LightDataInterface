using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    /// <summary>
    /// Support base class that provides access to current DB.
    /// </summary>
    public abstract class DataSessionAware<TDbContext> : IDataSessionAware
        where TDbContext : DbContext
    {
        protected DataSessionAware(string dataName = null)
        {
            DataName = dataName;
        }

        /// <summary>
        /// Returns the current named <see cref="DbContext"/> you can use in your repository implementations.
        /// </summary>
        protected TDbContext DbContext => this.GetDbContext<TDbContext>();

        protected IDataSession DataSession => this.GetDataSession();

        #region Implementation of IDataSessionAware

        /// <summary>
        /// Provides the data name that is used to obtain the current context in property <see cref="DbContext"/>.
        /// </summary>
        public string DataName { get; protected set; }

        #endregion
    }
}