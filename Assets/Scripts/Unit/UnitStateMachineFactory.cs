using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachineFactory
{
    private readonly Unit _unit;
    private readonly Transform _holdPoint;
    private readonly Base _base;

    public UnitStateMachineFactory(Unit unit, Transform holdPoint, Base @base)
    {
        _unit = unit;
        _holdPoint = holdPoint;
        _base = @base;
    }

    public StateMachine Create()
    {
        StateMachine stateMachine = new();

        List<IExitableState> states = new()
        {
            new IdleState(stateMachine, _unit),
            new PickUpResourceState(stateMachine, _unit, _holdPoint),
            new DropOffResourceState(stateMachine, _unit, _base),
            new BuildBaseState(stateMachine, _unit)
        };

        stateMachine.Initialize(states);
        stateMachine.ChangeState<IdleState>();
        return stateMachine;
    }
}
