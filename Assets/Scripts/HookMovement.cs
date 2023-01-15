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
    private FishStateManager fishScript;
    private Camera cam;
    private Rigidbody rb;
    private float i = 0.5f;
    bool hooked;
    bool caught;

    [SerializeField] private GameObject fish;
    [SerializeField] private LayerMask hitlayer;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Fixa så att hooken är en del av fishprefab ?
        rb = GetComponent<Rigidbody>();
        fishScript = fish.GetComponent<FishStateManager>();
        fishTransform = fish.transform;
        cam = Camera.current;
        hooked = false;
        caught = false;
    }
    private void Update()
    {
        if (caught)
        {
            
        }
        else if (!hooked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitlayer))
            {
                Vector3 hookToRay = ((hitData.point - transform.position) * speed);
                Vector3 targetPos = fishScript.GetTargetPos();
                Vector3 hookToTargetPos = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);
                rb.velocity = hookToRay;
            }
        } else
        {
            if (i <= 10) { i = i + Time.deltaTime * UnityEngine.Random.Range(0.8f,1.5f); }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, hitlayer))
            {
                Vector3 hookToRay = (hitData.point - transform.position) * speed * Vector3.Distance(hitData.point,transform.position);
                Vector3 targetPos = fishScript.GetTargetPos();
                Vector3 hookToTargetPos = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);
                rb.velocity = hookToRay + hookToTargetPos * i;
            }
        }
        

    }
    public void hook()
    {
        hooked = true;
    }

    public void unHook()
    {
        hooked = false;
    }
    public void fishCaught()
    {
        caught = true;
        gameObject.SetActive(false);
    }
    public void reseti()
    {
        i = 0.5f;
    }
}
