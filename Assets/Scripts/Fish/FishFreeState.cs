using Unity.VisualScripting;
using UnityEngine;

public class FishFreeState : FishBaseState
{
    //Variables
    private bool _getNewPos;

    public override void EnterState(FishStateManager fish)
    {

    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        fish.SwitchState(fish.hooked);
        fish.SetHookToCaught();
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (_getNewPos)
        {
            fish.CalculateNewTargetPos();
            _getNewPos= false;
        }

        else
        {
            Vector3 targetPos = fish.GetTargetPos();
            fish.transform.LookAt(targetPos);
            Vector3 fishToTargetPos = Vector3.Normalize(targetPos - fish.transform.position);
            fish.SetVelocity(fishToTargetPos);
            if (Vector3.Distance(new Vector3(fish.transform.position.x, 0, fish.transform.position.z), new Vector3(targetPos.x, 0, targetPos.z)) < 0.1f)
            {
                _getNewPos = true;
                fish.SetStartToCurrentPosition();
            }
        }
    }
}
