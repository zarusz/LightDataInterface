namespace LightDataInterface.Core
{
    /// <summary>
    /// Support base class for any respository implementation.
    /// </summary>
    public abstract class DataSessionAware : IDataSessionAware
    {
        protected DataSessionAware(string dataName = null)
        {
            DataName = dataName;
        }

        /// <summary>
        /// <see cref="IDataSession"/> obtained based on the <see cref="DataName"/> property.
        /// </summary>
        protected IDataSession DataSession => this.GetDataSession();

        #region Implementation of IDataSessionAware

        /// <summary>
        /// Provides the data name that is used to obtain the current <see cref="IDataSession"/>.
        /// </summary>
        public string DataName { get; protected set; }

        #endregion
    }
}