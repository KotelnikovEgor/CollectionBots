using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private readonly int _unitCost = 3;
    private readonly int _newBaseCost = 5;

    private Queue<Unit> _freeUnits = new();
    private int _totalUnitsCount;
    private bool _isColonizationInProgress;

    private ResourceRegistry _resourceRegistry;
    private Scanner _scanner;
    private Warehouse _warehouse;
    private Unit _unitPrefab;
    private Flag _flag;
    private BaseFactory _baseFactory;

    private void OnEnable()
    {
        if (_scanner != null)
            _scanner.ResourceFinded += SendUnitToCollectResource;
    }

    private void OnDisable()
    {
        if (_scanner != null)
            _scanner.ResourceFinded -= SendUnitToCollectResource;
    }

    public void Initialize(ResourceRegistry resourceRegistry, Scanner scanner, Warehouse warehouse, Unit unitPrefab, Flag flag, BaseFactory baseFactory)
    {
        _resourceRegistry = resourceRegistry;
        _scanner = scanner;
        _warehouse = warehouse;
        _unitPrefab = unitPrefab;
        _flag = flag;
        _baseFactory = baseFactory;

        _scanner.ResourceFinded -= SendUnitToCollectResource;
        _scanner.ResourceFinded += SendUnitToCollectResource;
    }

    public void AttachUnit(Unit unit)
    {
        unit.Initialize(this);
        _freeUnits.Enqueue(unit);
        _totalUnitsCount++;
    }

    public void SetFlag(Vector3 position)
    {
        if (_isColonizationInProgress)
            return;

        _flag.Place(position);
        TryExecutePriority();
    }

    public void OnBaseBuilt()
    {
        _flag.Hide();
        _isColonizationInProgress = false;
    }

    public void GetResource(Unit unit, Resource resource)
    {
        _warehouse.Place(resource, 1);
        _resourceRegistry.CompleteResource(resource);
        resource.Collect();
        _freeUnits.Enqueue(unit);
        TryExecutePriority();
    }

    private void TryExecutePriority()
    {
        if (_flag.IsPlaced)
        {
            if (!_isColonizationInProgress && _totalUnitsCount > 1 && _freeUnits.Count > 0 && _warehouse.TrySpendResources(_newBaseCost))
            {
                Unit unit = _freeUnits.Dequeue();
                _totalUnitsCount--;
                _isColonizationInProgress = true;
                unit.BuildBase(_flag.transform.position, _baseFactory);
            }
        }
        else
        {
            if (_warehouse.TrySpendResources(_unitCost))
            {
                SpawnNewUnit();
            }
        }
    }

    private void SpawnNewUnit()
    {
        Vector3 spawnPos = new(transform.position.x, _unitPrefab.transform.position.y, transform.position.z);
        Unit newUnit = Instantiate(_unitPrefab, spawnPos, Quaternion.identity);
        AttachUnit(newUnit);
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
