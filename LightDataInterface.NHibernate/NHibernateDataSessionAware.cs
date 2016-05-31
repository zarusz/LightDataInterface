using LightDataInterface.Core;
using NHibernate;

namespace LightDataInterface.NHibernate
{
    /// <summary>
    /// Support base class that provides access to current DB.
    /// </summary>
    public abstract class NHibernateDataSessionAware : DataSessionAware
    {
        protected NHibernateDataSessionAware(string dataName = null)
            : base(dataName)
        {
        }

        /// <summary>
        /// Returns the current named <see cref="ISession"/> you can use in your repository implementations.
        /// </summary>
        protected ISession Session => this.GetSession();
    }
}