using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HookMovement : MonoBehaviour
{

    private Vector3 screenpos;
    private Vector3 worldpos;
    [SerializeField] private LayerMask hitlayer;
     private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    
    State state;
    // Start is called before the first frame update
    void Start()
    {
        // Fixa så att hooken är en del av fishprefab ?
        cam = Camera.current;
        state = new Default();
    }
    private void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitlayer))
        {
            Vector3 ab = (-1 * transform.position) + hitData.point;
            Vector3 n = new Vector3(speed, speed, speed);
            Vector3 result = Vector3.Scale(ab, n);
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
