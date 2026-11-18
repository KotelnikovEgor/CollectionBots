using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;

    private Base _base;
    private IStateMachineUpdater _stateMachine;

    public event Action<Resource> TargetAcquired;
    public event Action<BuildOrderPayload> BuildCommandReceived;

    private void Update()
    {
        _stateMachine.UpdateState();
    }

    public void Initialize(Base @base)
    {
        _base = @base;

        UnitStateMachineFactory unitStateMachineFactory = new(this, _holdPoint, _base);
        _stateMachine = unitStateMachineFactory.Create();
    }

    public void CollectResource(Resource resource)
    {
        TargetAcquired?.Invoke(resource);
    }

    public void BuildBase(Vector3 position, BaseFactory baseFactory)
    {
        BuildOrderPayload order = new(position, baseFactory, _base);
        BuildCommandReceived?.Invoke(order);
    }
}
