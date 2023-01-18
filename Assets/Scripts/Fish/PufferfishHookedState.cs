using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferfishHookedState : FishBaseState
{
    public override void EnterState(FishStateManager fish)
    {
        fish.BeginDrawCircleSequence();
    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        
    }

    public override void UpdateState(FishStateManager fish)
    {
        fish.GetNewTargetPosOnCondition();
        fish.transform.LookAt(fish.GetTargetPos());
        Vector3 fishToHook = Vector3.Normalize((fish.GetHookTransform().position - new Vector3(0, 1f, 0)) - fish.transform.position);
        Vector3 fishToTargetPos = Vector3.Normalize(fish.GetTargetPos() - fish.transform.position);

        fish.SetVelocity(fishToHook * 3 + fishToTargetPos);
        if (fish.IsOutsideRange())
        {
            //Defeat
        }
    }
}
