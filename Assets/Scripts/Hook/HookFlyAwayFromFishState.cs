using UnityEngine;

public class HookFlyAwayFromFishState : HookBaseState
{
    float timer;
    public override void EnterState(HookStateManager hook)
    {
        timer = 0;
    }

    public override void UpdateState(HookStateManager hook)
    {
        if (timer <= 1.5) 
        { 
            timer += Time.deltaTime;
            Vector3 fishToHook = new Vector3(hook.transform.position.x - hook.GetFishTransform().position.x, 0, hook.transform.position.z - hook.GetFishTransform().position.z);
            hook.SetVelocity(Vector3.Normalize(fishToHook) * 4 - fishToHook * 2);
        } else { hook.SwitchState(hook.Free); }
        
    }
}
