using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private List<Unit> _initialUnits;

    private void Awake()
    {
        ResourceRegistry resourceRegistry = new();
        BaseFactory baseFactory = new(_basePrefab, _unitPrefab, resourceRegistry, _flagPrefab);

        Base startBase = baseFactory.Create(Vector3.zero);

        foreach (Unit unit in _initialUnits)
        {
            startBase.AttachUnit(unit);
        }
    }
}
