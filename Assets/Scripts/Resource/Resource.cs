using UnityEngine;

public class Resource : MonoBehaviour, IPayload
{
    [SerializeField] private ResourceType _type;

    public ResourceType Type => _type;

    public Transform Transform => transform;
}
