using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishColliderScript : MonoBehaviour
{

    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject fishPrefab;
    private GameObject fishSpawner;
    private Animator anim;
    private bool fly;
    int i = 0;

    private void Start()
    {
        anim = fish.GetComponent<Animator>();
        fishSpawner = GameObject.Find("/FishSpawner");
    }
    private void Update()
    {
        
        if (fly == true)
        {
            fish.transform.position = fish.transform.position + new Vector3(0, 0.05f, 0);
            i++;
            if (i > 120)
            {
                fishSpawner.GetComponent<FishSpawner>().spawn();
                Destroy(fishPrefab);
                fly = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        anim.enabled = false;
        fly = true;
        
        
    }
}
