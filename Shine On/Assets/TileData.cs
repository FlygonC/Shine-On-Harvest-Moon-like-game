using UnityEngine;
using System.Collections;

[System.Serializable]
public struct TilePosition
{
    public int x;
    public int y;
}

public class TileData {
    [Header("Tile:")]
    public TilePosition tilePos;
    public bool walkable = true;
}
