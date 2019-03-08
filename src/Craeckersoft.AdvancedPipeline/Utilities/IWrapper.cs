namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public interface IWrapper
    {
        object Item { get; }
    }

    public interface IWrapper<out T> : IWrapper
    {
        new T Item { get; }
    }
}