using System;

namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public interface IComponentInfo : IWrapper, IEquatable<IComponentInfo>
    {
        Type RequestType { get; }

        Type ResponseType { get; }

        Type NextRequestType { get; }

        Type NextResponseType { get; }

        object CreateInvoker(object next);
    }
}