using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FishFlyingState : FishBaseState
{
    public override void EnterState(FishStateManager fish)
    {

    }

    public override void OnTriggerEnter(FishStateManager fish)
    {

    }

    public override void UpdateState(FishStateManager fish)
    {
        if (Vector3.Distance(fish.transform.position, fish.GetTargetPos()) < 2.0f)
        {

            fish.transform.position = new Vector3(-0.5f, 5.559f, -4.645f);
            fish.transform.rotation = Quaternion.Euler(-100.036f, 37.608f, -23.65f);
        }
        else
        {
            fish.UpdateFlying();
        }
    }
}
