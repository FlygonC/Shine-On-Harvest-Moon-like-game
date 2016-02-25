using UnityEngine;
using System.Collections;

public struct TilePosition
{
    public int x;
    public int y;
}

[System.Serializable]
public class FarmTileData {
    
    public TilePosition tile;

    public float hydration = 0;
    public bool hydrated
    {
        get
        {
            return hydration > 0;
        }
    }
    public bool planted = false;
    public Crop crop = new Crop();
    
    public void Interact(PlayerControl.Tool _usedTool)
    {
        switch (_usedTool)
        {
            case PlayerControl.Tool.Hands:
                if (crop.stage == crop.identity.stages)
                {
                    Die(false);
                    GameObject.FindObjectOfType<PlayerControl>().tempMoney += crop.health;
                }
                break;
            case PlayerControl.Tool.WaterCan:
                if (!hydrated)
                {
                    hydration += 1;
                }
                break;
            case PlayerControl.Tool.Hoe:
                if (planted)
                {
                    Die(false);
                }
                break;
            case PlayerControl.Tool.Seed:
                if (!planted)
                {
                    planted = true;
                }
                break;
            default:
                break;
        }
    }

    public void PlantSeed(Plant _seed)
    {
        planted = true;
    }
    
    public void NextDay()
    {
        if (planted)
        {
            if (hydrated)
            {
                if (crop.stage < crop.identity.stages)
                {
                    crop.growth += 1;
                    if (crop.growth >= crop.identity.growTime)
                    {
                        crop.growth = 0;
                        crop.stage += 1;
                    }
                }
            }
            else
            {
                crop.health -= crop.identity.fragility;
                if (crop.health <= 0)
                {
                    Die(false);
                }
            }
        }
        hydration = 0;
    }
    
    public void Die(bool _leaveWeed)
    {
        planted = false;
    }
}

[System.Serializable]
public class Crop
{
    public Plant identity;
    public float health = 100;
    public float growth = 0;
    public int stage = 1;
}

[System.Serializable][CreateAssetMenu(fileName = "New Plant", menuName = "Plant", order = 1)]
public class Plant : ScriptableObject
{
    public int growTime;// Days per stage
    public int stages;
    public float fragility;
    public Mesh[] stagesMeshs;
}