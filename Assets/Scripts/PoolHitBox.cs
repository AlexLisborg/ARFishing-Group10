using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolHitBox : MonoBehaviour
{
    FishSpawner fish;
    HookSpawner hook;
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        fish.GetComponent<ScriptableObject>();    
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            camera1.SetActive(true); 
            camera2.SetActive(false); 
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            fish.spawn();
            hook.spawn();
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        fish.spawn();
        camera1.SetActive(false);
        camera2.SetActive(true);
        hook.spawn();
    }
}
