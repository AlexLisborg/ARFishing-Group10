using UnityEngine;

public abstract class HookBaseState
{
    public abstract void EnterState(HookStateManager hook);
    public abstract void UpdateState(HookStateManager hook);
}
