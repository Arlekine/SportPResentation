using System;
using System.Collections.Generic;

namespace SquidGameVR.App
{
    public class ServiceLocator : IServiceLocator, IDisposable
    {
        public static IServiceLocator Instance { get; private set; }

        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public ServiceLocator()
        {
            if (Instance != null)
                throw new Exception("ServiceLocator already exists!");

            Instance = this;
        }

        public void AddService<T>(T service)
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                throw new ArgumentException($"Service {type} already added");

            _services.Add(type, service);
        }

        public void Dispose()
        {
            Instance = null;
            _services.Clear();
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            if (_services.ContainsKey(type) == false)
                throw new ArgumentException($"Service {type} doesn't exist!");

            return (T)_services[type];
        }
    }
}