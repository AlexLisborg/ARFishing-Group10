using UnityEngine;

public class HookHookedState : HookBaseState
{
    float i = 0.5f;
    float timer = 0;
    float randomWiggleValue = 0;
    float targetWiggleValue = 0;

    public override void EnterState(HookStateManager hook)
    {
        Debug.Log("Switched State to hooked");
    }

    public override void UpdateState(HookStateManager hook)
    {
        if (timer <= 1.5) { 
            timer += Time.deltaTime;
            if (targetWiggleValue < randomWiggleValue) { randomWiggleValue -= Time.deltaTime*4; }
            else { randomWiggleValue += Time.deltaTime*4; }
        } 
        else { 
            timer = 0; 
            targetWiggleValue = UnityEngine.Random.Range(-5, 5);
        }
        if (i <= 10) { i = i + Time.deltaTime * UnityEngine.Random.Range(0.8f, 1.5f); }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hook.GetHitLayer()))
        {
            Vector3 hookToRay = (hitData.point - hook.transform.position) * hook.GetSpeed() * Vector3.Distance(hitData.point, hook.transform.position);
            Vector3 targetPos = hook.GetTargetPos();
            Vector3 hookToTargetPos = new Vector3(targetPos.x - hook.transform.position.x, 0, targetPos.z - hook.transform.position.z);
            Vector3 wiggleVector = Vector3.Normalize(hookToTargetPos) * randomWiggleValue;
            hook.SetVelocity(hookToRay + hookToTargetPos * i + wiggleVector);
        } //else { hook.SwitchState(hook.Default); }
    }
    public void ResetI() { i = 0.5f; }
}
