using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _initialUnits = new();

    private Queue<Unit> _freeUnits = new();
    private ResourceRegistry _resourceRegistry;
    private Scanner _scanner;
    private Warehouse _warehouse;

    private void OnEnable()
    {
        _scanner.ResourceFinded += SendUnitToCollectResource;
    }

    private void OnDisable()
    {
        _scanner.ResourceFinded -= SendUnitToCollectResource;
    }

    public void Initialize(ResourceRegistry resourceRegistry, Scanner scanner, Warehouse warehouse)
    {
        _resourceRegistry = resourceRegistry;
        _scanner = scanner;
        _warehouse = warehouse;

        _scanner.ResourceFinded -= SendUnitToCollectResource;
        _scanner.ResourceFinded += SendUnitToCollectResource;

        foreach (Unit unit in _initialUnits)
        {
            unit.Initialize(this);
            _freeUnits.Enqueue(unit);
        }
    }

    public void GetResource(Unit unit, Resource resource)
    {
        _warehouse.Place(resource, 1);
        _resourceRegistry.CompleteResource(resource);
        Destroy(resource.gameObject);
        _freeUnits.Enqueue(unit);
    }

    private void SendUnitToCollectResource()
    {
        while (_freeUnits.Count > 0)
        {
            Resource resource = _resourceRegistry.GetFreeResource();

            if (resource == null)
                break;

            Unit unit = _freeUnits.Dequeue();
            unit.CollectResource(resource);
        }
    }
}
