using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFishInitializer : MonoBehaviour
{

    [SerializeField] GameObject CatchFishGameObjectsPrefab;
    [SerializeField] GameObject Text;
    [SerializeField] LayerMask HitLayer;

    private bool _avaliable;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        MakeAvaliable();
        _inventoryManager = InventoryManager.Instance;
    }
    private void Update()
    {
        if (_avaliable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hitDataa, 100, HitLayer))
                {
                    CatchFishInitialize();

                }
            }
        }
    }

    public void CatchFishInitialize()
    {
        _avaliable = false;
        GameObject.Instantiate(CatchFishGameObjectsPrefab, transform.position, Quaternion.identity);
        Text.SetActive(false);
    }

    public void MakeAvaliable()
    {
        _avaliable = true;
        Text.SetActive(true);
    }
}
