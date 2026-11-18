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
        if (IsTargetReached(TargetResource.transform.position, _interactDistance))
        {
            PickUp();
            StateMachine.ChangeState<DropOffResourceState, Resource>(TargetResource);
            return;
        }

        Move(TargetResource.transform.position, _speed);
    }

    private void PickUp()
    {
        TargetResource.transform.SetParent(_holdPoint);
        TargetResource.transform.localPosition = Vector3.zero;
    }
}
