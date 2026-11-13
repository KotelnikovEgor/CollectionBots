using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Warehouse _warehouse;

    private void Awake()
    {
        ResourceRegistry resourceRegistry = new();

        _warehouse.Initialize();
        _scanner.Initialize(resourceRegistry);
        _base.Initialize(resourceRegistry, _scanner, _warehouse);
    }
}
