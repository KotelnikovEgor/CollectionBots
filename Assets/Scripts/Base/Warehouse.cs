using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    private Dictionary<ResourceType, int> _resourcesAmount = new();

    public int TotalAmount => _resourcesAmount.Values.Sum();

    public event Action<ResourceType, int> ResourceAmountChanged;

    public void Initialize()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _resourcesAmount[type] = 0;
        }
    }

    public void Place(Resource resource, int amount)
    {
        ResourceType type = resource.Type;
        _resourcesAmount[type] += amount;
        ResourceAmountChanged?.Invoke(type, _resourcesAmount[type]);
    }

    public bool TrySpendResources(int requiredAmount)
    {
        if (TotalAmount < requiredAmount)
            return false;

        int remainingToSpend = requiredAmount;
        List<ResourceType> types = new(_resourcesAmount.Keys);

        foreach (ResourceType type in types)
        {
            if (remainingToSpend <= 0)
                break;

            int available = _resourcesAmount[type];

            if (available > 0)
            {
                int spent = Mathf.Min(available, remainingToSpend);
                _resourcesAmount[type] -= spent;
                remainingToSpend -= spent;

                ResourceAmountChanged?.Invoke(type, _resourcesAmount[type]);
            }
        }

        return true;
    }
}
