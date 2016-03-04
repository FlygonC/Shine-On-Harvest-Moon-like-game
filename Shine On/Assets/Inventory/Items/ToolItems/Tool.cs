using UnityEngine;
using System.Collections;

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