using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hookPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        Instantiate(hookPrefab,transform.position,Quaternion.identity);
    }
}
