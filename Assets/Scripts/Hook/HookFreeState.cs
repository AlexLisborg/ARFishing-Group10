using UnityEngine;

public class HookFreeState : HookBaseState
{
    public override void EnterState(HookStateManager hook)
    {
        Debug.Log("Changed State to Free");
    }

    public override void UpdateState(HookStateManager hook)
    {
        Camera cam = hook.GetCamera();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hook.GetHitLayer()))
        {
            Vector3 hookToRay = ((hitData.point - hook.transform.position) * hook.GetSpeed());
            hook.SetVelocity(hookToRay);
        } //else { hook.SwitchState(hook.Default); }
    }
}
