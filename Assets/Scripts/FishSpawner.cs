using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private List<GameObject> fishList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawn()
    {
        Instantiate(fishPrefab,transform.position,Quaternion.identity);
    }

    public void kill()
    {
        Destroy(fishPrefab);
    }
}
