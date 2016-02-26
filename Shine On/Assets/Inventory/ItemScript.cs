using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite icon;

    public bool useable;
    public bool consumable;
    [Range(1,20)]
    public int stackSize;

    public void UseItem(GameObject _target)
    {

    }
    public string WriteTooltip()
    {
        return "Tool Tip";
    }
}