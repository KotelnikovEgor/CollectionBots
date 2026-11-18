using UnityEngine;

public class BuildOrderPayload : IPayload
{
    public Vector3 BuildPosition { get; }
    public BaseFactory BaseFactory { get; }
    public Base OldBase { get; }

    public BuildOrderPayload(Vector3 buildPosition, BaseFactory baseFactory, Base oldBase)
    {
        BuildPosition = buildPosition;
        BaseFactory = baseFactory;
        OldBase = oldBase;
    }
}
