using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnClick : MonoBehaviour
{
    public Item item;
    InventoryManager im;
    private void Awake()
    {
        im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    public void sellThisItem()
    {
        im.sellItem(item);
    }
}
