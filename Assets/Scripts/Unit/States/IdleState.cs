public class IdleState : BaseState, IEnterableState
{
    public IdleState(IStateChanger stateMachine, Unit unit) : base(stateMachine, unit) { }

    public void Enter()
    {
        Unit.TargetAcquired += ChangeStateToCollect;
        Unit.BuildCommandReceived += ChangeStateToBuild;
    }

    public override void Exit()
    {
        Unit.TargetAcquired -= ChangeStateToCollect;
        Unit.BuildCommandReceived -= ChangeStateToBuild;
    }

    private void ChangeStateToCollect(Resource resource)
    {
        StateMachine.ChangeState<PickUpResourceState, Resource>(resource);
    }

    private void ChangeStateToBuild(BuildOrderPayload order)
    {
        StateMachine.ChangeState<BuildBaseState, BuildOrderPayload>(order);
    }
}
