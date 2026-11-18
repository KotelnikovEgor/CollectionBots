public class BuildBaseState : BaseState, IEnterablePayloadState<BuildOrderPayload>, IUpdateState
{
    private readonly float _speed = 1.5f;
    private readonly float _interactDistance = 0.4f;

    private BuildOrderPayload _order;

    public BuildBaseState(IStateChanger stateMachine, Unit unit) : base(stateMachine, unit) { }

    public void Enter(BuildOrderPayload payload)
    {
        _order = payload;
    }

    public override void Exit()
    {
        _order = null;
    }

    public void Update()
    {
        if (IsTargetReached(_order.BuildPosition, _interactDistance))
        {
            Build();
            return;
        }

        Move(_order.BuildPosition, _speed);
    }

    private void Build()
    {
        Base newBase = _order.BaseFactory.Create(_order.BuildPosition);
        _order.OldBase.OnBaseBuilt();
        newBase.AttachUnit(Unit);
    }
}
