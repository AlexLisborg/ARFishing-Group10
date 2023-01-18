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
    [SerializeField] GameObject BasicFish;
    [SerializeField] GameObject Pufferfish;
    [SerializeField] GameObject PrefabParent;
    [SerializeField] GameObject DefaultHitBox;
    [SerializeField] GameObject DeepHitBox;
    [SerializeField] GameObject MasterHitBox;
    [SerializeField] GameObject NoBaitHitBox;
    [SerializeField] BoxCollider BasicFishHitBox;
    [SerializeField] BoxCollider PufferfishHitBox;

    //State variable
    private FishBaseState _state;

    //States
    public FishFreeState Free = new FishFreeState();
    public FishHookedState Hooked = new FishHookedState();
    public FishFlyingState Flying = new FishFlyingState();
    public PufferfishFreeState PufferfishFree = new PufferfishFreeState();
    public PufferfishHookedState PufferfishHooked = new PufferfishHookedState();

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
    private bool _useActiveBait;
    private Bait _activeBait;
    private int _currentFish;

    private Rigidbody _rigidBody;
    private PufferfishScript _pufferfishScript;
    private InventoryManager _inventoryManager;
    private CatchFishInitializer _catchFishInitializer;
    private Vector3 _middleOfPool;
    

    [System.Obsolete]
    void Awake()
    {
        
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        
        _activeBait = _inventoryManager.activeBait;

        // If you have enough of the active bait.
        if (_inventoryManager.baits[_inventoryManager.activeBait.id] > 0)
        {
            Debug.Log("11");
            _useActiveBait = true;
            if (_activeBait.id == 0 || _activeBait.id == 2)
            {
                Debug.Log("1.31");
                DefaultHitBox.SetActive(true);
                DeepHitBox.SetActive(false);
                MasterHitBox.SetActive(false);
                NoBaitHitBox.SetActive(false);
                Debug.Log("1.32");
            }
            
            else if (_activeBait.id == 3)
            {
                Debug.Log("1.21");
                DefaultHitBox.SetActive(false);
                DeepHitBox.SetActive(false);
                MasterHitBox.SetActive(true);
                NoBaitHitBox.SetActive(false);
                Debug.Log("1.22");
            }
            else if (_activeBait.id == 1)
            {
                Debug.Log("1.11");
                DefaultHitBox.SetActive(false);
                DeepHitBox.SetActive(true);
                MasterHitBox.SetActive(false);
                NoBaitHitBox.SetActive(false);
                Debug.Log("1.12");
            }
            Debug.Log("12");
        }
        else {
            Debug.Log("Expected");
            _useActiveBait &= false;
            DefaultHitBox.SetActive(false);
            DeepHitBox.SetActive(false);
            MasterHitBox.SetActive(false);
            NoBaitHitBox.SetActive(true);
        }


            _pufferfishScript = Pufferfish.GetComponent<PufferfishScript>();
        _rigidBody= GetComponent<Rigidbody>();
        float random = Random.Range(0, 10);
        Debug.Log(random);
        if(random <= 7) { 
            _state = Free;
            BasicFish.SetActive(true);
            Pufferfish.SetActive(false);
            BasicFishHitBox.enabled = true;
            PufferfishHitBox.enabled = false;
            _currentFish = 0;
        } else { 
            _state = PufferfishFree;
            BasicFish.SetActive(false);
            Pufferfish.SetActive(true);
            BasicFishHitBox.enabled = false;
            PufferfishHitBox.enabled = true;
            _currentFish = 1;
        }
        _catchFishInitializer = GameObject.Find("CatchFishInitializer").GetComponent<CatchFishInitializer>();
        _middleOfPool = GameObject.Find("CatchFishInitializer").transform.position;
        _targetPos = _middleOfPool;
    }
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 30) { timer = 0; }
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
        var newX = Random.Range(_middleOfPool.x - 1.4f, _middleOfPool.x + 2.5f);
        var newZ = Random.Range(_middleOfPool.z - 3.0f, _middleOfPool.z +1.6f);
        var newY = Random.Range(_middleOfPool.y - 2.5f, _middleOfPool.y + 0.2f);
        _curSpeed = Random.Range(_minSpeed, _maxSpeed);
        _targetPos = new Vector3(newX, newY, newZ);
        Debug.Log("Getting new pos : " + newX + " , " + newZ + " , " + newY);
        HookStateManager.ResetI();

    }

    public Vector3 GetTargetPos() { return _targetPos; }
    public void SetVelocity(Vector3 velocity) { _rigidBody.velocity = velocity; }
    public void SetStartToCurrentPosition() { _start = transform.position; }
    public void BeginDrawCircleSequence() { LineController.BeginDrawCircleSequence(); }
    public Transform GetHookTransform() { return HookTransform; }
    public bool IsCaught() { return LineController.IsCaught(); }
    public void SetHookToCaught() { HookStateManager.SwitchState(HookStateManager.Hooked); }
    public bool IsOutsideRange() { Debug.Log(LineController.GetRadius()); return Vector3.Distance(transform.position, LineController.GetCenterPos()) > LineController.GetRadius() + 0.5f;  }
    
    public Bait GetActiveBait() { return _activeBait; }
    public void UpdateFlying()
    {
        _time += Time.deltaTime;
        _speed += _acceleration;
        _targetPos = new Vector3(Camera.main.transform.position.x + 0.7f, Camera.main.transform.position.y - 0.4f, Camera.main.transform.position.z);
        _pos = Vector3.Lerp(_start, _targetPos, _time * _speed);
        _pos.y += Curve.Evaluate(_time);
        transform.position = _pos;
    }

    public void GetNewTargetPosOnCondition()
    {
        Debug.Log(_targetPos);
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(_targetPos.x, 0, _targetPos.z)) < 0.5f) 
        { 
            CalculateNewTargetPos();
            SetStartToCurrentPosition();
        }
    }

    public void PufferFishAttack()
    {
        HookStateManager.PushAway();
    }
    public void CatchFish()
    {
        Item fishItem = transform.GetChild(_currentFish).gameObject.GetComponent<FishObjectScript>().GetItem();
        GameObject.Find("InventoryManager").GetComponent<InventoryManager>().addItem(fishItem);
        Destroy(GameObject.FindGameObjectWithTag("DuringCatchSequence"));
        _catchFishInitializer.MakeAvaliable();
    }

    public void SelfDestruct()
    {
        Destroy(PrefabParent);
        _catchFishInitializer.MakeAvaliable();
    }

    public void InflatePufferfish() { _pufferfishScript.Inflate(); }
    public void DeflatePufferfish() { _pufferfishScript.Deflate(); }
}
