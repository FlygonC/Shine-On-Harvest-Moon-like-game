using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour {

    public PlayerControl player;
    public FarmSquare squarePrefab;
    public GroundItem dropItem;
    public Produce eggggg;
    public Plant defplnt;

    public float tempClock = 1;
    public float timer = 0;
    public int day = 1;

    public const int farmRows = 10;
    public const int farmColumns = 10;
    public FarmTileData[,] farm;

    public FarmSquare[,] tiles;

    // Use this for initialization
    void Start () {
        // Initialize data
        farm = new FarmTileData[farmColumns, farmRows];
        for (int y = 0; y < farmRows; y++)
        {
            for (int x = 0; x < farmColumns; x++)
            {
                farm[x, y] = new FarmTileData();
                farm[x, y].tilePos.x = x;
                farm[x, y].tilePos.y = y;
                farm[x, y].crop.identity = defplnt;
            }
        }

        LoadFarm();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer -= 1;
            tempClock += 1;
            if (tempClock >= 25)
            {
                //tempClock -= 24;
                //day++;
                NextDay();
            }
        }

        /*if (PlayerControl.ThePlayer.interact)
        {
            foreach (FarmTileData i in farm)
            {
                if (player.targetTile.Equals(i.tilePos))
                {
                    //Debug.Log("thing");
                    i.Interact(player.equipedTool);
                }
            }
        }*/
	}

    [ContextMenu("Next Day")]
    public void NextDay()
    {
        tempClock = 1;
        day++;
        for (int y = 0; y < farmRows; y++)
        {
            for (int x = 0; x < farmColumns; x++)
            {
                farm[x, y].NextDay();
            }
        }

        Chicken[] chics = FindObjectsOfType<Chicken>();
        foreach (Chicken i in chics)
        {
            GroundItem newEgg = (GroundItem)Instantiate(dropItem);
            newEgg.position = i.tilePos;
            newEgg.item.identity = eggggg;
            newEgg.item.count = 1;
            newEgg.item.data = (int)Random.Range(1, 5);
        }
    }

    [ContextMenu("Load")]
    public void LoadFarm()
    {
        tiles = new FarmSquare[farmColumns, farmRows];
        for (int y = 0; y < farmRows; y++)
        {
            for (int x = 0; x < farmColumns; x++)
            {
                tiles[x, y] = (FarmSquare)Instantiate(squarePrefab/*, new Vector3(x, 0, y), Quaternion.identity*/);
                tiles[x, y].data = farm[x, y];
                tiles[x, y].name = "Farm Tile " + x + ", " + y;
            }
        }
    }
    [ContextMenu("Unload")]
    public void UnloadFarm()
    {
        for (int y = 0; y < farmRows; y++)
        {
            for (int x = 0; x < farmColumns; x++)
            {
                GameObject.Destroy(tiles[x, y].gameObject);
            }
        }
    }
}
