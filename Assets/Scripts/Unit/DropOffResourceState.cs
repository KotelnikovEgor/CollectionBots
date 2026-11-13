using UnityEngine;

public class DropOffResourceState : BaseState, IEnterablePayloadState<Resource>, IUpdateState
{
    private readonly Base _base;
    private readonly float _speed = 0.5f;
    private readonly float _interactDistance = 1f;

    public DropOffResourceState(IStateChanger stateMachine, Unit unit, Base baseTransform) : base(stateMachine, unit)
    {
        _base = baseTransform;
    }

    public void Enter(Resource payload)
    {
        TargetResource = payload;
    }

    public override void Exit()
    {
        TargetResource = null;
    }

    public void Update()
    {
        if (Vector3.Distance(Unit.transform.position, _base.transform.position) <= _interactDistance)
        {
            DropOff();
            StateMachine.ChangeState<IdleState>();
            return;
        }

        Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, _base.transform.position, _speed * Time.deltaTime);
        Unit.transform.LookAt(_base.transform);
    }

    private void DropOff()
    {
        _base.GetResource(Unit, TargetResource);
    }
}
