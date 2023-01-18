using Unity.VisualScripting;
using UnityEngine;

public class FishFreeState : FishBaseState
{
    private float _timer;
    public override void EnterState(FishStateManager fish)
    {
        _timer = 0;
    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        fish.SwitchState(fish.Hooked);
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (_timer >= 30)
        {
            fish.SwitchState(fish.Hooked);
        } else
        {
            _timer += Time.deltaTime;
            fish.GetNewTargetPosOnCondition();

            Vector3 targetPos = fish.GetTargetPos();
            fish.transform.LookAt(targetPos);
            Vector3 fishToTargetPos = Vector3.Normalize(targetPos - fish.transform.position);
            fish.SetVelocity(fishToTargetPos);
        }
        
    }
}
