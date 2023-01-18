using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishList;

    // Start is called before the first frame update
    void Start()
    {
        InitializeFish();
    }

    public void InitializeFish()
    {
        var i = Random.Range(0, fishList.Count - 1);
        Debug.Log(i);
        GameObject fish = Instantiate(fishList[1], GameObject.Find("FishRotationFix").transform.position, Quaternion.identity);
        fish.transform.parent = GameObject.Find("FishRotationFix").transform;
    }
}
