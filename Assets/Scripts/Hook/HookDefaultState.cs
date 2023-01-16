using UnityEngine;

public class HookDefaultState : HookBaseState
{
    public override void EnterState(HookStateManager hook)
    {
        Debug.Log("Changed State to Default");
    }

    public override void UpdateState(HookStateManager hook)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hook.GetHitLayer()))
        {
            hook.SwitchBackState();
        } else
        {
            hook.SetVelocity(hook.transform.position - hook.GetDefaultPos());
        }
    }
}
