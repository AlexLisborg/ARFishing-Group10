using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HookMovement : MonoBehaviour
{

    private Vector3 screenpos;
    private Vector3 worldpos;
    private Transform fishTransform;
    private FishScript fishScript;
    private Camera cam;
    private Rigidbody rb;
    private float i = 0.5f;

    [SerializeField] private GameObject fish;
    [SerializeField] private LayerMask hitlayer;
    [SerializeField] private float speed;
    
    State state;
    // Start is called before the first frame update
    void Start()
    {
        // Fixa så att hooken är en del av fishprefab ?
        rb = GetComponent<Rigidbody>();
        fishScript = fish.GetComponent<FishScript>();
        fishTransform = fish.transform;
        cam = Camera.current;
        state = new Default();
    }
    private void Update()
    {
        if (i <= 5) { i = i + Time.deltaTime * 0.3f; }
        Debug.Log(i);

        
        Vector3 vTowardsFish = Vector3.Normalize( fishScript.fishMoveTowardsPosition - transform.position);
        //fishRb.velocity = new Vector3(transform.position.x - fishTransform.position.x, transform.position.y - fishTransform.position.y, transform.position.z - fishTransform.position.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitlayer))
        {
            Vector3 ab = (-1 * transform.position) + hitData.point;
            Vector3 n = new Vector3(speed, speed, speed);
            Vector3 result = new Vector3(ab.x * speed + vTowardsFish.x * i, ab.y, ab.z * speed + vTowardsFish.z * i);
            rb.velocity = result;
        }

    }
    private interface State
    {
        void Update();
    }

    private class Off 
    {
    }
    private class Default : HookMovement, State
    {
        // Update is called once per frame
        void HookMovement.State.Update()
        {
            

        }
    }

    private class HookedfishDefault
    {

    }
    

    

    
}
