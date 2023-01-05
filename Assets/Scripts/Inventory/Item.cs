using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string name;
    public int value;
    public Sprite icon;
    public int inventorySpot;
}
