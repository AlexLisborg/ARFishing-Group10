using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public List<Bait> baitsObjects = new List<Bait>();

    public int money;
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();
    public int[] baits = new int[4];
    public Bait activeBait;

    public TextMeshProUGUI moneyCounter;
    public Transform itemContent;
    public GameObject inventoryItem;
    public Toggle sell;

    private void Awake()
    {
        baits[0] = 0;
        baits[1] = 0;
        baits[2] = 0;
        baits[3] = 0;
        money = 0;
        SelectDefaultBait();
        Instance = this;
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

    public void buyBait(int baitId)
    {
        
        switch (baitId)
        {
            case 0:
                if (money >= 10)
                {
                    money -= 10;
                    baits[baitId] += 1;
                }
                break;
            case 1:
                if (money >= 20)
                {
                    money -= 20;
                    baits[baitId] += 1;
                }
                break;
            case 2:
                if (money >= 50)
                {
                    money -= 50;
                    baits[baitId] += 1;
                }
                break;
            case 3:
                if (money >= 500)
                {
                    money -= 500;
                    baits[baitId] += 1;
                }
                break;
        }
        moneyCounter.text = money.ToString();
        updateBaitCounters();
    }

    public void setActiveBait(Bait bait) { activeBait = bait; }

    public void SelectDefaultBait() { activeBait = baitsObjects[0]; Debug.Log(activeBait); }
    public void SelectDeepBait() { activeBait = baitsObjects[1]; Debug.Log(activeBait); }
    public void SelectStrongBait() { activeBait = baitsObjects[2]; Debug.Log(activeBait); }
    public void SelectMasterBait() { activeBait = baitsObjects[3]; Debug.Log(activeBait); }

    public void updateBaitCounters()
    {
        GameObject[] counters = GameObject.FindGameObjectsWithTag("BaitCounter");
        foreach (GameObject obj in counters)
        {
            TextMeshProUGUI counter = obj.GetComponent<TextMeshProUGUI>();
            counter.text = baits[Array.IndexOf(counters, obj)].ToString();
        }
    }
}
