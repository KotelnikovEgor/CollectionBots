using UnityEngine;

public class BaseFactory
{
    private readonly Base _basePrefab;
    private readonly Unit _unitPrefab;
    private readonly ResourceRegistry _resourceRegistry;
    private readonly Flag _flagPrefab;

    public BaseFactory(Base basePrefab, Unit unitPrefab, ResourceRegistry resourceRegistry, Flag flagPrefab)
    {
        _basePrefab = basePrefab;
        _unitPrefab = unitPrefab;
        _resourceRegistry = resourceRegistry;
        _flagPrefab = flagPrefab;
    }

    public Base Create(Vector3 position)
    {
        Base newBase = Object.Instantiate(_basePrefab, position, Quaternion.identity);

        Scanner scanner = newBase.GetComponent<Scanner>();
        Warehouse warehouse = newBase.GetComponent<Warehouse>();

        Flag personalFlag = Object.Instantiate(_flagPrefab);
        personalFlag.gameObject.SetActive(false);

        warehouse.Initialize();
        scanner.Initialize(_resourceRegistry);
        newBase.Initialize(_resourceRegistry, scanner, warehouse, _unitPrefab, personalFlag, this);

        return newBase;
    }
}
