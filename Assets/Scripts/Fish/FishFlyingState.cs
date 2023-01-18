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
        fish.FlyFishTowardCamera(fish.transform.position);
    }
}
