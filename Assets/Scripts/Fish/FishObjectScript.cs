using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishObjectScript : MonoBehaviour
{
    [SerializeField]private Item _item;
    public Item GetItem()
    {
        return _item;
    }
}
