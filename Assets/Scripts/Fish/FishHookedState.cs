using Unity.VisualScripting;
using UnityEngine;

public class FishHookedState : FishBaseState
{
    private bool _getNewPos;
    private float _timer;
    public override void EnterState(FishStateManager fish)
    {
        fish.SetHookToCaught();
        fish.BeginDrawCircleSequence();
        _timer = 0;
    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (_timer > 3) { fish.CalculateNewTargetPos(); }
        fish.transform.LookAt(fish.GetTargetPos());
        Vector3 fishToHook = Vector3.Normalize((fish.GetHookTransform().position - new Vector3(0, 1f, 0)) - fish.transform.position);
        Vector3 fishToTargetPos = Vector3.Normalize(fish.GetTargetPos() - fish.transform.position);

        fish.SetVelocity(fishToHook * 3 + fishToTargetPos);
        if (fish.IsCaught())
        {
            fish.ConsumeActiveBait();
            fish.caughtAudio.Play();
            fish.DestroyHook();
            fish.SwitchState(fish.Flying);
            
        }
        if (fish.IsOutsideRange())
        {
            fish.ConsumeActiveBait();
            fish.SelfDestruct();
        }
    }
}
