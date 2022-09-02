using System.Collections.Concurrent;
using System;

namespace processes.Engine
{
    public class SharedContext: ISharedContext
    {
        private ConcurrentDictionary<string, object> _dictionary;

        public SharedContext() => this._dictionary = new ConcurrentDictionary<string, object>();

        public object GetResult(Guid key)
        {
            if(this._dictionary.TryGetValue(key.ToString(), out object @result)) return @result;
            throw new InvalidOperationException($"No result found for task with id {key}");
        }
        public void AddResult(Guid key, object value) => this._dictionary.TryAdd(key.ToString(), value);
    }
}