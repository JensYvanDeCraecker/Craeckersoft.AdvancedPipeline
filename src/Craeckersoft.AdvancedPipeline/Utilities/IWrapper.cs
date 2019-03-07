namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public interface IWrapper<out T>
    {
        T Item { get; }
    }
}