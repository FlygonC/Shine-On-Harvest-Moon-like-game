﻿using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour {

    public PlayerControl player;
    public FarmSquare squarePrefab;
    public Plant defaultPlant;

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
                farm[x, y].tile.x = x;
                farm[x, y].tile.y = y;
                farm[x, y].crop.identity = defaultPlant;
            }
        }

        LoadFarm();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer -= 0.5f;
            tempClock += 1;
            if (tempClock >= 25)
            {
                tempClock -= 24;
                day++;
                NextDay();
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (player.targetTile.x >= 0 && player.targetTile.x <= farmColumns - 1 && player.targetTile.y >= 0 && player.targetTile.y <= farmRows - 1)
            {
                farm[player.targetTile.x, player.targetTile.y].Interact(player.equipedTool);
            }
        }
	}

    [ContextMenu("Next Day")]
    public void NextDay()
    {
        for (int y = 0; y < farmRows; y++)
        {
            for (int x = 0; x < farmColumns; x++)
            {
                farm[x, y].NextDay();
            }
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
                tiles[x, y] = (FarmSquare)Instantiate(squarePrefab, new Vector3(x, 0, y), Quaternion.identity);
                tiles[x, y].data = farm[x, y];
                tiles[x, y].name = "Tile " + x + ", " + y;
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
