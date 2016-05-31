namespace LightDataInterface.Core
{
    /// <summary>
    /// The interface of a consumer of the current <see cref="IDataSession"/>.
    /// This should be implemented by the repository/factory implementation in your data access layer.
    /// </summary>
    public interface IDataSessionAware
    {
        string DataName { get; }
    }
}