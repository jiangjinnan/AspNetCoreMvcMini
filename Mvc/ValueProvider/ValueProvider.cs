using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Mvc
{
    public class ValueProvider : IValueProvider
    {
        private readonly NameValueCollection _values;

        public static ValueProvider Empty = new ValueProvider(new NameValueCollection());
        public ValueProvider(NameValueCollection values)
        => _values = new NameValueCollection(StringComparer.OrdinalIgnoreCase) { values };
        public ValueProvider(IEnumerable<KeyValuePair<string, StringValues>> values)
        {
            _values = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in values)
            {
                foreach (var value in kv.Value)
                {
                    _values.Add(kv.Key.Replace("-", ""), value);
                }
            }
        }

        public bool ContainsPrefix(string prefix)
        {
            foreach (string key in _values.Keys)
            {
                if (key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValues(string name, out string[] value)
        {
            value = _values.GetValues(name);
            return value?.Any() == true;
        }
    }
}
