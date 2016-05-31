using System.Data.Entity;
using LightDataInterface.Core;

namespace LightDataInterface.EntityFramework
{
    /// <summary>
    /// Support base class for your repository/factory implementation on Entity Framework.
    /// </summary>
    public abstract class EfDataSessionAware<TDbContext> : DataSessionAware
        where TDbContext : DbContext
    {
        protected EfDataSessionAware(string dataName = null)
            : base(dataName)
        {
        }

        /// <summary>
        /// Returns the current named <see cref="DbContext"/> you can use in your repository implementations.
        /// </summary>
        protected TDbContext DbContext => this.GetDbContext<TDbContext>();
    }
}