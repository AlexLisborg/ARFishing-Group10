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
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    private Rigidbody rb;
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        screenpos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(screenpos);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitlayer))
        {
            Vector3 ab = (-1 * transform.position) + hitData.point;
            Vector3 n = new Vector3(speed, speed, speed);
            Vector3 result = Vector3.Scale(ab,n);
            rb.velocity = result;
        }
        
    }
}
