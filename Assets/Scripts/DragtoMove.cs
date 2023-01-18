using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragtoMove : MonoBehaviour
{
    private Touch Touch;
    private float speedModifier;
    private GameObject ob;

    
    void Start()
    {
        ob = GameObject.Find("CatchFishObjects");
        speedModifier = 0.006f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && ob == null) 
        {
            Touch = Input.GetTouch(0);

            if (Touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + Touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z + Touch.deltaPosition.y * speedModifier);
            }
        }
    }
}
