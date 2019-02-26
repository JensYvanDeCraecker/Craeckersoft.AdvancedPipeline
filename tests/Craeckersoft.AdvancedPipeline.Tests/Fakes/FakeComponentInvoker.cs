namespace Craeckersoft.AdvancedPipeline.Tests.Fakes
{
    public class FakeComponentInvoker : IComponentInvoker<string, string>
    {
        public string Invoke(string request, IPipelineInvocationContext invocationContext)
        {
            return request ?? "Success";
        }
    }
}