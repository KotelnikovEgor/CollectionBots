using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPayload
{
    [SerializeField] private ResourceType _type;

    public ResourceType Type => _type;

    public Transform Transform => transform;

    public event Action<Resource> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
