using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFishInitializer : MonoBehaviour
{

    [SerializeField] GameObject catchFishGameObjectsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        catchFishInitialize(); 
    }

    public void catchFishInitialize()
    {
        GameObject.Instantiate(catchFishGameObjectsPrefab, transform.position, Quaternion.identity);
    }
}
