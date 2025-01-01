using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubsystemLocator : MonoBehaviour
{
    private static readonly Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();

    public static T GetSubsystem<T>() where T : class
    {
        var concreteType = services.Keys.FirstOrDefault(type => typeof(T).IsAssignableFrom(type));

        if (concreteType != null && services.TryGetValue(concreteType, out var service))
        {
            return service as T;
        }

        throw new Exception($"Service of type {typeof(T).Name} not found.");
    }

    public static void RegisterSubsystem<T>(T service) where T : class
    {
        var type = service.GetType();
        if (!services.ContainsKey(type))
        {
            services.Add(type, service);
        }
        else
        {
            throw new System.Exception($"Service of type {type.Name} is already registered.");
        }
    }

    public static void UnregisterSubsystem<T>(T service) where T : class
    {
        var type = service.GetType();
        if (services.ContainsKey(type))
        {
            services.Remove(type);
        }
    }

    public static IEnumerable<T> GetAllSubsystems<T>() where T : class
    {
        return services.Values.OfType<T>();
    }

    public static void UnregisterAllSubsystems()
    {
        services.Clear();
    }
}