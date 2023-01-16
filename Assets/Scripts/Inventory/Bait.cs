using UnityEngine;
[CreateAssetMenu(fileName = "New Bait", menuName = "Item/Create New Bait")]

public class Bait : ScriptableObject
{
    public int id;
    public string name;
    public int price;
    public int quality;
}
