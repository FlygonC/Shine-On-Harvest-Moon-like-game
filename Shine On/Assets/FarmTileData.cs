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
                if (crop.fullGrown)
                {
                    Harvest();
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
                    PlantSeed();
                }
                break;
            default:
                break;
        }
    }

    public void PlantSeed(/*Plant _seed*/)
    {
        planted = true;
        crop.health = 100;
        crop.stage = 1;
    }
    
    public void NextDay()
    {
        if (planted)
        {
            if (hydrated)
            {
                // If not Fully Grown
                if (!crop.fullGrown)
                {
                    // Grow
                    crop.growth += 1;
                    // Next Stage growth
                    if (crop.growth >= crop.identity.growTime)
                    {
                        crop.growth = 0;
                        crop.stage += 1;
                    }
                }
                if (crop.health < 100)
                {
                    crop.health = Mathf.Min(crop.health + 10, 100);
                }
            }
            else
            {
                // If not Watered, Lose health
                crop.health -= crop.identity.fragility;
                // Die if 0 health...
                if (crop.health <= 0)
                {
                    Die(false);
                }
            }
        }
        hydration = 0;
    }

    public void Harvest()
    {
        GameObject.FindObjectOfType<PlayerControl>().tempMoney += crop.health;
        GameObject.FindObjectOfType<Inventory>().PickUpItem(crop.identity.yield);
        if (crop.identity.dieOnHarvest)
        {
            Die(false);
        }
        else
        {
            crop.growth = 0;
            crop.stage = crop.identity.harvestRevertStage;
        }
    }
    
    public void Die(bool _leaveWeed)
    {
        planted = false;
        crop.health = 0;
        crop.growth = 0;
        crop.stage = 0;
    }
}

[System.Serializable]
public class Crop
{
    public Plant identity;
    public float health = 100;
    public float growth = 0;
    public int stage = 1;

    public bool fullGrown
    {
        get
        {
            return stage >= identity.stages;
        }
    }
}

[System.Serializable][CreateAssetMenu(fileName = "New Plant", menuName = "Plant", order = 1)]
public class Plant : ScriptableObject
{
    public int growTime;// Days per stage of growth
    public int stages;// Number of Stages of growth
    public float fragility;// Health lost in harmfull conditions
    public Mesh[] stagesMeshs;// Visual Meshes for stages
    public bool dieOnHarvest;// If dies When Harvested
    public int harvestRevertStage;// Stage to revert to on harvest
    public Item yield;// Item you get from Harvesting
}