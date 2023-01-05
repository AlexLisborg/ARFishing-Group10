using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int money;
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public TextMeshProUGUI moneyCounter;
    public Transform itemContent;
    public GameObject inventoryItem;
    public Toggle sell;

    private void Awake()
    {
        money = 0;
        Instance = this;
    }

    public Item getItem(int invSpot)
    {
        foreach (Item item in items)
        {
            if (item.inventorySpot == invSpot)
            {
                return item;
            }
        }
        return null;
    }

    public void addItem(Item item)
    {
        items.Add(item);
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
        listItems();
    }

    public void sellItem(Item item)
    {
        money = money + item.value;
        removeItem(item);
        moneyCounter.text = money.ToString();
    }
    public void openInventory()
    {
        sell.isOn = false;
        listItems();
    }
    public void listItems()
    {
        
        moneyCounter.text = money.ToString();
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem,itemContent);
            obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            obj.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
            obj.GetComponent<Button>().interactable = sell.isOn;
            obj.GetComponent<ItemOnClick>().item = item;
        }
    }

    public void onToggle()
    {
        
        if (sell.isOn)
         {
            foreach (Transform item in itemContent)
            {
                item.GetComponent<Button>().interactable = true;
            }
         } else
         {
            foreach (Transform item in itemContent)
            {
                item.GetComponent<Button>().interactable = false;
            }
         }
    }
}
