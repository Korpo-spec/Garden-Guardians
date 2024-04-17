
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    //public int value;
    public Sprite icon;

    public bool isStackable;

}
