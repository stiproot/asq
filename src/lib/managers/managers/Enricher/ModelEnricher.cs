using System.Collections.Generic;
using System.Reflection;
using System;

namespace managers.Enricher
{
    public static class ObjectEnricher
    {
        public static void EnrichObject<T>(
            ref T subject,
            IEnumerable<(string, Func<T, object>)> enrichementRules
        )
        {
            foreach(var rule in enrichementRules)
            {
                var val = rule.Item2.Invoke(subject);

                var property = subject.GetType().GetProperty(rule.Item1, BindingFlags.Instance | BindingFlags.Public);

                property.SetValue(subject, val, null);
            }
        }
    }
}