using System.Linq;

namespace Craeckersoft.AdvancedPipeline.Tests.Fakes
{
    public class FakeFilter : IFilter<string, string>
    {
        public string Invoke(string request, IPipelineInvocationContext invocationContext)
        {
            return request != null ? new string(request.Reverse().ToArray()) : null;
        }
    }
}