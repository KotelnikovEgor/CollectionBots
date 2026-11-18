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
        if (IsTargetReached(_base.transform.position, _interactDistance))
        {
            Resource deliveredResource = TargetResource;
            StateMachine.ChangeState<IdleState>();
            DropOff(deliveredResource);
            return;
        }

        Move(_base.transform.position, _speed);
    }

    private void DropOff(Resource resource)
    {
        _base.GetResource(Unit, resource);
    }
}
