using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    [SerializeField] Transform hookTransform;
    public Vector3 fishMoveTowardsPosition = new Vector3(0,0,0);
    protected float minSpeed = 0.005f;
    protected float maxSpeed = 0.01f;
    protected bool getNewPos = true;
    protected Vector3 targetPos;
    protected float curSpeed;
    protected float rangeDiff = 1.0f;
    protected bool onHook = false;
    protected bool isCaught = false;
    public GameObject fishSpawner;
    private float acceleration = 0.008f;
    private float speed = 0.02f;
    private Rigidbody rb;
    bool fly;
    bool swimming;
    float n = 1;

    public AnimationCurve curve;
    public Vector3 pos;
    private bool atMiddle = false;

    Vector3 start;
    float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        start = transform.position;
        fly = false;
        swimming = true;
    }

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        if (swimming)
        {

            if (getNewPos)
            {
                var newX = Random.Range(-1.4f, 2.5f);
                var newZ = Random.Range(-3.0f, 1.6f);
                var newY = Random.Range(-2.5f, 0.2f);
                n = 1;
                
                if (IsValidNewPosition(newX, newZ))
                {
                    curSpeed = Random.Range(minSpeed, maxSpeed);
                    targetPos = new Vector3(newX, newY, newZ);
                    fishMoveTowardsPosition = targetPos;
                    getNewPos = false;
                    
                    Debug.Log("Getting new pos : " + newX + " , " + newZ + " , " + newY);
                }
            }
            else
            {
                if (n <= 30)
                {
                    n = n + 0.01f;
                }
                
                transform.LookAt(targetPos);
                rb.velocity = (Vector3.Scale(Vector3.Normalize(new Vector3((hookTransform.position.x - transform.position.x), ((hookTransform.position.y - 0.3f) - transform.position.y), (hookTransform.position.z - transform.position.z))), new Vector3(2,2,2)) + Vector3.Normalize(new Vector3(targetPos.x - transform.position.x, targetPos.y - transform.position.y, targetPos.z - transform.position.z)) * 1);
                if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPos.x, 0 , targetPos.z)) < 0.1f)
                {
                    getNewPos = true;
                    start = transform.position;
                }
            }
        }
        else if (fly)
        {
            time += Time.deltaTime;
            speed += acceleration;
            targetPos = new Vector3(Camera.main.transform.position.x + 0.7f, Camera.main.transform.position.y - 0.4f, Camera.main.transform.position.z);
            pos = Vector3.Lerp(start, targetPos, time * speed);
            pos.y += curve.Evaluate(time);
            transform.position = pos;
            if (Vector3.Distance(transform.position, targetPos) < 2.0f)
            {
                print("at camera");
                fly = false;
                transform.position = new Vector3(-0.5f, 5.559f, -4.645f);
                transform.rotation = Quaternion.Euler(-100.036f, 37.608f, -23.65f);
            }
        }
    }

    bool IsValidNewPosition(float newX, float newZ)
    {
        var xPos = transform.position.x;
        var zPos = transform.position.z;

        if (xPos < 0 && newX > 0 && (FloatAbs(xPos) + newX > 0.5f) && (FloatAbs(zPos) + newZ > rangeDiff))
        {
            return true;
        }
        else if (xPos > 0 && newX < 0 && (FloatAbs(newX) + xPos > 0.5f) && (FloatAbs(newZ) + zPos > rangeDiff))
        {
            return true;
        }
        else
        {
            if (xPos < newX && FloatAbs(xPos) - FloatAbs(newX) > 0.5 && FloatAbs(zPos) - FloatAbs(newZ) > rangeDiff)
            {
                return true;
            }
            else if (xPos > newX && FloatAbs(newX) - FloatAbs(xPos) > 0.5 && FloatAbs(newZ) - FloatAbs(zPos) > rangeDiff)
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

    private void OnTriggerEnter(Collider other)
    {
        swimming = false;
        fly = true;
        start = transform.position;
        //fishSpawner.GetComponent<FishSpawner>().spawn();
    }
}