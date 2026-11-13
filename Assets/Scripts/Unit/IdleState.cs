public class IdleState : BaseState, IEnterableState
{
    public IdleState(IStateChanger stateMachine, Unit unit) : base(stateMachine, unit) { }

    public void Enter()
    {
        Unit.TargetAcquired += ChangeStateToCollect;
    }

    public override void Exit()
    {
        Unit.TargetAcquired -= ChangeStateToCollect;
    }

    private void ChangeStateToCollect(Resource resource)
    {
        StateMachine.ChangeState<PickUpResourceState, Resource>(resource);
    }
}
