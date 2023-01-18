using UnityEngine;

public class PufferfishFreeState : FishBaseState
{

    //Variables
    private float _range = 1f;
    private float _timer = 0;
    private bool _isAngry;
    public override void EnterState(FishStateManager fish)
    {
        _isAngry = false;
        fish.DeflatePufferfish();
    }

    public override void OnTriggerEnter(FishStateManager fish)
    {
        if (_timer < 10 && fish.GetActiveBait().id != 2)
        {
            fish.InflatePufferfish();
            _isAngry = true;
            fish.PufferFishAttack();
        } else { fish.SwitchState(fish.Hooked); }
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (_isAngry){_timer += Time.deltaTime;}
        if (_timer >= 10) { fish.DeflatePufferfish(); }

        fish.GetNewTargetPosOnCondition();
        Vector3 targetPos = fish.GetTargetPos();
        fish.transform.LookAt(targetPos);
        Vector3 fishToTargetPos = Vector3.Normalize(targetPos - fish.transform.position);
        fish.SetVelocity(fishToTargetPos);

    }
}
