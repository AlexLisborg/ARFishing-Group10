using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public List<Item> avaliableItems;

    private string filePathInventory;
    private string filePathMoney;
    private string filePathBait;

    private void Awake()
    {
        filePathInventory = Application.persistentDataPath + "/inventory.txt";
        filePathBait = Application.persistentDataPath + "/money.txt";
        filePathMoney = Application.persistentDataPath + "/bait.txt";
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

    public void SelectDefaultBait() { activeBait = baitsObjects[0]; }
    public void SelectDeepBait() { activeBait = baitsObjects[1];  }
    public void SelectStrongBait() { activeBait = baitsObjects[2]; }
    public void SelectMasterBait() { activeBait = baitsObjects[3]; }

    public void updateBaitCounters()
    {
        GameObject[] counters = GameObject.FindGameObjectsWithTag("BaitCounter");
        foreach (GameObject obj in counters)
        {
            TextMeshProUGUI counter = obj.GetComponent<TextMeshProUGUI>();
            counter.text = baits[Array.IndexOf(counters, obj)].ToString();
        }
    }
    public void Save()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Item item in items) 
        {
            sb.Append(item.id + System.Environment.NewLine);
        }
        System.IO.File.WriteAllText(filePathInventory,sb.ToString());
        sb.Clear();
        foreach (int i in baits)
        {
            sb.Append(i + System.Environment.NewLine);
        }
        System.IO.File.WriteAllText(filePathBait, sb.ToString());
        System.IO.File.WriteAllText(filePathMoney, money.ToString());
    }
    public void Load()
    {
        string content = System.IO.File.ReadAllText(filePathInventory);
        string[] listOfItems = content.Split(new char[] { '\r', '\n' });
        foreach (string itemId in listOfItems)
        {
            addItem(avaliableItems[itemId.ToIntArray()[0]]);
        }
        string contentBaits = System.IO.File.ReadAllText(filePathBait);
        string[] listOfBaits = contentBaits.Split(new char[] { '\r', '\n' });
        for (int i = 0; i < listOfBaits.Length; i++)
        {
            baits[i] = listOfBaits[i].ToIntArray()[0];
        }
        money = System.IO.File.ReadAllText(filePathMoney).ToIntArray()[0];
        updateBaitCounters();
        
    }
    public void AddMoney(int amount) { money += amount; }
}
