using UnityEngine;
using System.Collections;


[System.Serializable]
public class FarmTileData : TileData {
    [Header("Farm Tile:")]
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
    
    // Interactions
    public void Interact()
    {
        if (PlayerControl.ThePlayer.heldItem.identity == null && PlayerControl.ThePlayer.holding == false)
        {
            if (crop.fullGrown)
            {
                Harvest();
            }
        }
        if (PlayerControl.ThePlayer.heldItem.identity as Tool)
        {
            switch(PlayerControl.ThePlayer.heldItem.GetTool.toolType)
            {
                case PlayerControl.Tool.WaterCan:
                    hydration = 1;
                    break;
                case PlayerControl.Tool.Hoe:
                    Die(false);
                    break;
            }
        }
        if (PlayerControl.ThePlayer.heldItem.identity as SeedBag)
        {
            if (!planted)
            {
                PlantSeed(PlayerControl.ThePlayer.heldItem.GetSeedBag.plants);
                PlayerControl.ThePlayer.InvRef.handHeldItem.count--;
            }
        }
    }

    // Other functions
    public void PlantSeed(Plant _seed)
    {
        crop.identity = _seed;
        planted = true;
        crop.health = 100;
        crop.stage = 1;
        crop.growth = 0;
        crop.age = 0;
    }
    
    public void NextDay()
    {
        crop.age++;
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
        //GameObject.FindObjectOfType<PlayerControl>().tempMoney += crop.health;
        GameObject.FindObjectOfType<Inventory>().PickUpItem(crop.identity.yield, 1, QualityCheck());
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

    float QualityCheck()
    {
        int ret = 0;//rotted <25
        if (crop.health > 25)
        {
            ret++;//poor 25-75
        }
        if (crop.health > 75)
        {
            ret++;//average 76-125
        }
        if (crop.health > 125)
        {
            ret++;//good 126-175
        }
        if (crop.health > 175)
        {
            ret++;//superb >176
        }
        return ret;
    }
}

[System.Serializable]
public class Crop
{
    public Plant identity;
    public float health = 100;
    public float growth = 0;
    public int stage = 1;
    public int age = 0;

    public bool fullGrown
    {
        get
        {
            return stage >= identity.stages;
        }
    }
}

