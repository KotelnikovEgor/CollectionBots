using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private List<Resource> _resourceTypes;
    [SerializeField] private float _rate;
    [SerializeField] private int _maxActive;

    [SerializeField] private float _xPosition;
    [SerializeField] private float _zPosition;

    [SerializeField] private float _spawnCheckRadius;
    [SerializeField] private LayerMask _obstacleLayer;

    private readonly float _yPosition = 0.1f;
    private readonly int _maxSpawnAttempts = 10;

    private Dictionary<Resource, ObjectPool<Resource>> _pools;
    private int _activeCount;

    private void Start()
    {
        InstantiatePools();
        StartCoroutine(SpawnCoroutine());
    }

    /*public void ReleaseResource(Resource prefab, Resource instance)
    {
        if (_pools.TryGetValue(prefab, out var pool))
        {
            pool.Release(instance);
        }
    }*/

    private void InstantiatePools()
    {
        _pools = new Dictionary<Resource, ObjectPool<Resource>>();

        foreach (Resource resourcePrefab in _resourceTypes)
        {
            Resource prefab = resourcePrefab;

            _pools[prefab] = new ObjectPool<Resource>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (resource) => 
                {
                    _activeCount++;
                    resource.gameObject.SetActive(true);
                },
                actionOnRelease: (resource) => 
                {
                    _activeCount--;
                    resource.gameObject.SetActive(false);
                },
                actionOnDestroy: (resource) => Destroy(resource.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: _maxActive
            );
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds wait = new(_rate);

        while (enabled)
        {
            yield return wait;

            if (_activeCount < _maxActive)
                TrySpawnRandomResource();
        }
    }

    private void TrySpawnRandomResource()
    {
        if (_resourceTypes.Count == 0) 
            return;

        if (TryGetFreePosition(out Vector3 freePosition))
        {
            Resource randomPrefab = GetRandomResource();
            Resource spawnedResource = _pools[randomPrefab].Get();
            spawnedResource.transform.position = freePosition;
        }
    }

    private bool TryGetFreePosition(out Vector3 validPosition)
    {
        for (int i = 0; i < _maxSpawnAttempts; i++)
        {
            Vector3 candidatePosition = GetRandomPosition();

            if (!Physics.CheckSphere(candidatePosition, _spawnCheckRadius, _obstacleLayer))
            {
                validPosition = candidatePosition;
                return true;
            }
        }

        validPosition = Vector3.zero;
        return false;
    }

    private Vector3 GetRandomPosition()
    {
        return new(Random.Range(-_xPosition, _xPosition), _yPosition, Random.Range(-_zPosition, _zPosition));
    }

    private Resource GetRandomResource()
    {
        return _resourceTypes[Random.Range(0, _resourceTypes.Count)];
    }
}
