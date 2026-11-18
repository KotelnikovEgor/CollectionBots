using UnityEngine;

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

    protected void Move(Vector3 targetPosition, float speed)
    {
        Vector3 flatTarget = new(targetPosition.x, Unit.transform.position.y, targetPosition.z);

        Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, flatTarget, speed * Time.deltaTime);
        Unit.transform.LookAt(flatTarget);
    }

    protected bool IsTargetReached(Vector3 targetPosition, float interactDistance)
    {
        Vector3 flatTarget = new(targetPosition.x, Unit.transform.position.y, targetPosition.z);
        return Vector3.Distance(Unit.transform.position, flatTarget) <= interactDistance;
    }
}
