using UnityEngine;

public class PickUpResourceState : BaseState, IEnterablePayloadState<Resource>, IUpdateState
{
    private readonly Transform _holdPoint;
    private readonly float _speed = 1f;
    private readonly float _interactDistance = 0.4f;

    public PickUpResourceState(IStateChanger stateMachine, Unit unit, Transform holdPoint) : base(stateMachine, unit) 
    {
        _holdPoint = holdPoint;
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
        if (Vector3.Distance(Unit.transform.position, TargetResource.transform.position) <= _interactDistance)
        {
            PickUp();
            StateMachine.ChangeState<DropOffResourceState, Resource>(TargetResource);
            return;
        }

        Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, TargetResource.transform.position, _speed * Time.deltaTime);
        Unit.transform.LookAt(TargetResource.transform);
    }

    private void PickUp()
    {
        TargetResource.transform.SetParent(_holdPoint);
        TargetResource.transform.localPosition = Vector3.zero;
    }
}
