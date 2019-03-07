using System.Collections;
using System.Collections.Generic;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public class FakeInvocationContext : IInvocationContext
    {
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item) { }

        public void Clear() { }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return false;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) { }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return false;
        }

        public int Count { get; } = 0;

        public bool IsReadOnly { get; } = true;

        public void Add(string key, object value) { }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public bool Remove(string key)
        {
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            value = null;
            return false;
        }

        public object this[string key]
        {
            get
            {
                return null;
            }
            set { }
        }

        public ICollection<string> Keys { get; } = null;

        public ICollection<object> Values { get; } = null;
    }
}