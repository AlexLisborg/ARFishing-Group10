using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] GameObject follow;
    LineRenderer ln;
    
    bool caught;
    bool active;

    [SerializeField] private int pointCount;
    [SerializeField] private float radiusMultiplier;
    [SerializeField] private float shrinkSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        ln = GetComponent<LineRenderer>();
        ln.positionCount = pointCount;
        active = false;
        caught = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (caught == false)
            {
                
                //Draw circle with *pointCount* number of points
                for (int i = 0; i < pointCount; i++)
                {
                    float v = (Mathf.PI / (pointCount / 2)) * i;
                    ln.SetPosition(i, radiusMultiplier * new Vector3(Mathf.Sin(v), 0, Mathf.Cos(v)));
                }

                //Decreace radius bu shrinkspeed
                radiusMultiplier = radiusMultiplier - shrinkSpeed * Time.deltaTime;
                //If radius is at the limit ( 2 ), boolean caught is set to true
                if (radiusMultiplier <= 0.8f) { caught = true; }

            }
            else
            {
                if (ln.positionCount != 0) { ln.positionCount = 0; active = false; }
            }
        }
    }

    public void BeginDrawCircleSequence()
    {
        transform.position = follow.transform.position;
        ln = GetComponent<LineRenderer>();
        ln.positionCount = pointCount; 
        active = true; 
        caught = false; 
    }

    public void ExitDrawCircleSequence()
    {
        ln.positionCount = 0; active = false;
    }

    public float GetRadius(){return radiusMultiplier;}
    public Vector3 GetCenterPos() { return transform.position; }
    public bool IsCaught()
    {
        Debug.Log(caught);
        return caught;
    }
}
