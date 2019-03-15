using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline;
using Craeckersoft.AdvancedPipeline.Components;

namespace ConsoleTest
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            MiddlewareComponent<string, string, string, string> component = Component.FromMiddleware(new SomeMiddleware());
            component.Invoking += (sender, eventArgs) =>
            {
                Console.WriteLine("Component invocation started. Request: {0}", eventArgs.Request);
                eventArgs.Request = "A";
            };
            component.Invoked += (sender, eventArgs) =>
            {
                Console.WriteLine("Component invocation ended. Response: {0}", eventArgs.Response);
                eventArgs.Response = "B";
            };
            string response = await component.GetInvoker(ComponentInvoker.FromDelegate<string, string>((request, ctx) => Task.FromResult(request + "S"))).InvokeAsync("R", new SomeInvocationContext());
            Console.WriteLine(response);
        }

        private class SomeMiddleware : IMiddleware<string, string, string, string>
        {
            public async Task<string> InvokeAsync(string request, IInvocationContext invocationContext, IComponentInvoker<string, string> next)
            {
                return await next.InvokeAsync(request, invocationContext) + "T";
            }
        }

        private class SomeInvocationContext : IInvocationContext
        {
            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public bool Contains(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public bool Remove(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }

            public int Count { get; }

            public bool IsReadOnly { get; }

            public void Add(string key, object value)
            {
                throw new NotImplementedException();
            }

            public bool ContainsKey(string key)
            {
                throw new NotImplementedException();
            }

            public bool Remove(string key)
            {
                throw new NotImplementedException();
            }

            public bool TryGetValue(string key, out object value)
            {
                throw new NotImplementedException();
            }

            public object this[string key]
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public ICollection<string> Keys { get; }

            public ICollection<object> Values { get; }
        }
    }
}