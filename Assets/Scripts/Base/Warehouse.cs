using System;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    private Dictionary<ResourceType, int> _resourcesCount = new();

    public event Action<ResourceType, int> ResourceCountChanged;

    public void Initialize()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _resourcesCount[type] = 0;
        }
    }

    public void Place(Resource resource, int count)
    {
        ResourceType type = resource.Type;
        _resourcesCount[type] += count;
        ResourceCountChanged?.Invoke(type, _resourcesCount[type]);
    }
}
