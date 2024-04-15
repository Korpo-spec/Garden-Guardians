
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int amountInStack;
    public int value;
    public readonly int stackSize=8;
    public bool isFull => amountInStack >= stackSize;

    public Sprite icon;

}
