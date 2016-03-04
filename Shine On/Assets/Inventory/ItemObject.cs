using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class ItemObject : ScriptableObject
{
    //public enum ItemType { Produce, Tool, Seeds, Food };
    //public enum ItemTool { NotTool = 0, Watercan, Hoe, Seeds };

    //public ItemType type;
    [SerializeField]
    private string _ItemName;
    public string itemName
    {
        get
        {
            return _ItemName;
        }
    }
    [SerializeField]
    private Sprite _Icon;
    public Sprite icon
    {
        get
        {
            return _Icon;
        }
    }
    [SerializeField]
    private bool _Stackable;
    public bool stackable
    {
        get
        {
            return _Stackable;
        }
    }
    //public bool consumable;

    //public ItemTool tool;


}

[CreateAssetMenu(fileName = "New Seed", menuName = "SeedBag", order = 2)]
public class SeedBag : ItemObject
{
    [SerializeField]
    private Plant _Plant;
    public Plant plants
    {
        get
        {
            return _Plant;
        }
    }
}
[CreateAssetMenu(fileName = "New Produce", menuName = "Produce", order = 3)]
public class Produce : ItemObject
{
    
}
[CreateAssetMenu(fileName = "New Tool", menuName = "Tool", order = 4)]
public class Tool : ItemObject
{
    [SerializeField]
    private PlayerControl.Tool _ToolType;
    public PlayerControl.Tool toolType
    {
        get
        {
            return _ToolType;
        }
    }
}