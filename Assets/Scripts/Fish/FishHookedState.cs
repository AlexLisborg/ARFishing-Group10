using Unity.VisualScripting;
using UnityEngine;

public class FishHookedState : FishBaseState
{
    private bool _getNewPos;
    public override void EnterState(FishStateManager fish)
    {
        fish.BeginDrawCircleSequence();
    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (_getNewPos)
        {
            fish.CalculateNewTargetPos();
            _getNewPos= false;
        } else
        {
            fish.transform.LookAt(fish.GetTargetPos());
            Vector3 fishToHook = Vector3.Normalize((fish.GetHookTransform().position - new Vector3(0, 1f, 0)) - fish.transform.position);
            Vector3 fishToTargetPos = Vector3.Normalize(fish.GetTargetPos() - fish.transform.position);

            fish.SetVelocity(fishToHook * 3 + fishToTargetPos);
            if (Vector3.Distance(new Vector3(fish.transform.position.x, 0, fish.transform.position.z), new Vector3(fish.GetTargetPos().x, 0, fish.GetTargetPos().z)) < 0.5f)
            {
                _getNewPos = true;
                fish.SetStartToCurrentPosition();
            }
        }
        if (fish.IsOutsideRange())
        {

        }
    }
}
