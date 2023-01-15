using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookStateManager : MonoBehaviour
{
    //States
    public HookHookedState Hooked = new HookHookedState();
    public HookFreeState Free = new HookFreeState();
    public HookDefaultState Default = new HookDefaultState();

    private HookBaseState _state;
    private HookBaseState _previousState;
  
    //Serializables
    [SerializeField] FishStateManager FishStateManager;
    [SerializeField] LayerMask HitLayer;

    //Variables
    private float _speed;
    private Vector3 _defaultPos;

    private Rigidbody _rigidbody;
    private Camera _camera;


    void Start()
    {
        _state = Free;
        _rigidbody= GetComponent<Rigidbody>();
        _defaultPos = _rigidbody.transform.position;
        _camera = Camera.main;
        _speed = 2;
    }

    void Update()
    {
        _state.UpdateState(this);
    }

    public void SwitchState(HookBaseState state) 
    { 
        _previousState = state;
        _state = state; 
        _state.EnterState(this); 
    }

    public void ResetI() 
    {
        if(_state.Equals(Hooked))
        {
            HookHookedState current = (HookHookedState)_state;
            current.ResetI();
        }
    }

    public float GetSpeed() { return _speed; }
    public LayerMask GetHitLayer() { return HitLayer; }
    public Vector3 GetTargetPos() { return FishStateManager.GetTargetPos(); }
    public void SetVelocity(Vector3 velocity) { _rigidbody.velocity = velocity; }
    public Vector3 GetDefaultPos() { return _defaultPos; }
    public void SwitchBackState() { SwitchState(_previousState); }
    public Camera GetCamera() { return _camera; }
}
