using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FishStateManager : MonoBehaviour
{
    //Serializeables
    [SerializeField] HookStateManager HookStateManager;
    [SerializeField] Transform HookTransform;
    [SerializeField] LineController LineController;
    [SerializeField] AnimationCurve Curve;

    //State variable
    private FishBaseState _state;

    //States
    public FishFreeState swimming = new FishFreeState();
    public FishHookedState hooked = new FishHookedState();
    public FishFlyingState flying = new FishFlyingState();

    //Variables
    private float _curSpeed;
    private float _minSpeed;
    private float _maxSpeed;
    private float _rangeDiff = 1f;
    private float _time;
    private float _speed = 0.02f;
    private float _acceleration = 0.008f;
    private Vector3 _targetPos;
    private Vector3 _start;
    private Vector3 _pos;


    private Rigidbody _rigidBody;


    void Start()
    {
        _state = swimming;
        _rigidBody= GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(_state.ToString());
        _state.UpdateState(this);
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        _state.OnTriggerEnter(this);
    }

    public void SwitchState(FishBaseState state)
    {
        _state = state;
        state.EnterState(this);
    }

    public bool IsValidNewPosition(float newX, float newZ)
    {
        var xPos = transform.position.x;
        var zPos = transform.position.z;

        if (xPos < 0 && newX > 0 && (FloatAbs(xPos) + newX > 0.5f) && (FloatAbs(zPos) + newZ > _rangeDiff))
        {
            return true;
        }
        else if (xPos > 0 && newX < 0 && (FloatAbs(newX) + xPos > 0.5f) && (FloatAbs(newZ) + zPos > _rangeDiff))
        {
            return true;
        }
        else
        {
            if (xPos < newX && FloatAbs(xPos) - FloatAbs(newX) > 0.5 && FloatAbs(zPos) - FloatAbs(newZ) > _rangeDiff)
            {
                return true;
            }
            else if (xPos > newX && FloatAbs(newX) - FloatAbs(xPos) > 0.5 && FloatAbs(newZ) - FloatAbs(zPos) > _rangeDiff)
            {
                return true;
            }
        }
        return false;
    }

    float FloatAbs(float f)
    {
        if (f < 0)
        {
            return (-1 * f);
        }
        return f;
    }

    public void CalculateNewTargetPos()
    {
        var newX = Random.Range(-1.4f, 2.5f);
        var newZ = Random.Range(-3.0f, 1.6f);
        var newY = Random.Range(-2.5f, 0.2f);

        if (IsValidNewPosition(newX, newZ))
        {
            _curSpeed = Random.Range(_minSpeed, _maxSpeed);
            _targetPos = new Vector3(newX, newY, newZ);
            Debug.Log("Getting new pos : " + newX + " , " + newZ + " , " + newY);
            HookStateManager.ResetI();
        }
    }

    public Vector3 GetTargetPos() { return _targetPos; }
    public void SetVelocity(Vector3 velocity) { _rigidBody.velocity = velocity; }
    public void SetStartToCurrentPosition() { _start = transform.position; }
    public void BeginDrawCircleSequence() { LineController.BeginDrawCircleSequence(); }
    public Transform GetHookTransform() { return HookTransform; }
    public bool IsCaught() { return LineController.IsCaught(); }
    public void SetHookToCaught() { HookStateManager.SwitchState(HookStateManager.Hooked); }
    public bool IsOutsideRange() { return Vector3.Distance(transform.position, LineController.GetCenterPos()) > LineController.GetRadius(); }
    public void UpdateFlying()
    {
        _time += Time.deltaTime;
        _speed += _acceleration;
        _targetPos = new Vector3(Camera.main.transform.position.x + 0.7f, Camera.main.transform.position.y - 0.4f, Camera.main.transform.position.z);
        _pos = Vector3.Lerp(_start, _targetPos, _time * _speed);
        _pos.y += Curve.Evaluate(_time);
        transform.position = _pos;
    }




}
