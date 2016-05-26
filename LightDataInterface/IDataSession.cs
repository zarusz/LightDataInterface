namespace LightDataInterface
{
    /// <summary>
    /// The session established with the data store.
    /// </summary>
    public interface IDataSession : IUnitOfWorkFactory
    {
        bool AutoFlush { get; set; }
        /// <summary>
        /// Forces all in-memory changes to be reflected in the underlying store.
        /// </summary>
        void Flush();
    }
}