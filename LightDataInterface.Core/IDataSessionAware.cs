namespace LightDataInterface.Core
{
    /// <summary>
    /// The interface of a consumer of the current <see cref="IDataSession"/>.
    /// This should be implemented by the Repository or Factory implementation in your data access layer.
    /// </summary>
    public interface IDataSessionAware
    {
        string DataName { get; }
    }
}