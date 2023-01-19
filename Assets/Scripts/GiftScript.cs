using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour
{
    public LayerMask HitLayer;
    public AudioSource sound;
    public GameObject prefab;
    private float _counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= 10)
        {
            foreach (Transform trs in prefab.transform)
            {
                Destroy(trs);
            }
            if (prefab != null) { Destroy(prefab); }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out RaycastHit hitDataa, 100, HitLayer))
            {
                GameObject.Find("InventoryManager").GetComponent<InventoryManager>().AddMoney(100);
            }
        }
    }
}
