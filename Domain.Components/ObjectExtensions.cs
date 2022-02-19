using Domain.Components.Abstractions;
using System.Reflection;

namespace Domain.Components
{
    // Nicked from https://stackoverflow.com/a/4944547/1720761
    public static class ObjectExtensions
    {
        public static IAuthorizationContext? ToObject(this IDictionary<string, object> source)
        {
            if (source.TryGetValue("__type", out var __type) && __type is string typeName)
            {
                var @interface = typeof(IAuthorizationContext);

                var type = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .FirstOrDefault(p =>
                        @interface.IsAssignableFrom(p)
                        && p.IsClass
                        && p.Name == typeName);

                var instance = (IAuthorizationContext)Activator.CreateInstance(type);

                foreach (var item in source)
                {
                    type.GetProperty(item.Key)
                        .SetValue(instance, item.Value, null);
                }

                return instance;
            }

            return null;
        }

        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, IAuthorizationContext, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                    .GetProperty(item.Key)
                    .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(
            this IAuthorizationContext source, 
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source
                .GetType()
                .GetProperties(bindingAttr)
                .ToDictionary(
                    propInfo => propInfo.Name,
                    propInfo => propInfo.GetValue(source, null)
                );
        }
    }
}
