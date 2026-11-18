using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _rate;
    [SerializeField] private Vector3 _area;
    [SerializeField] private LayerMask _resourceLayer;

    private ResourceRegistry _resourceRegistry;

    public event Action ResourceFinded;

    private void Start()
    {
        StartCoroutine(ScanCoroutine());
    }

    public void Initialize(ResourceRegistry resourceRegistry)
    {
        _resourceRegistry = resourceRegistry;
    }

    private IEnumerator ScanCoroutine()
    {
        WaitForSeconds wait = new(_rate);

        while (enabled)
        {
            yield return wait;
            Scan();
        }
    }

    private void Scan()
    {
        List<Resource> foundResources = new();
        Collider[] hits = Physics.OverlapBox(transform.position, _area, Quaternion.identity, _resourceLayer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
            {
                foundResources.Add(resource);
            }
        }

        _resourceRegistry.RegisterScannedResources(foundResources);

        if (foundResources.Count > 0)
            ResourceFinded?.Invoke();
    }
}
