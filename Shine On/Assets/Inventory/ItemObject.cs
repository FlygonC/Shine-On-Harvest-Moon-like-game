using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class ItemObject : ScriptableObject
{
    public enum ItemType { Produce, Tool, Seeds, Food };
    public enum ItemTool { NotTool = 0, Watercan, Hoe, Seeds };

    public ItemType type;
    
    public string itemName;
    public bool useable;
    public bool consumable;
    public int stackSize;

    public ItemTool tool;
    public Sprite icon;

    public void UseItem(GameObject _target)
    {

    }
    public string WriteTooltip()
    {
        return itemName;
    }
}