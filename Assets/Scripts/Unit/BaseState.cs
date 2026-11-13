public abstract class BaseState : IExitableState
{
    protected readonly IStateChanger StateMachine;
    protected readonly Unit Unit;

    protected Resource TargetResource;

    protected BaseState(IStateChanger stateMachine, Unit unit)
    {
        StateMachine = stateMachine;
        Unit = unit;
    }

    public abstract void Exit();
}
