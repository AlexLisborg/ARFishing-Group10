using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fishPrefab, transform.position, Quaternion.identity);
    }

    public void spawn()
    {
        Instantiate(fishPrefab,transform.position,Quaternion.identity);
    }
}
