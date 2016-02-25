using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour {

    public PlayerControl player;
    public FarmSquare squarePrefab;
    public Vector2 playerTargetTile
    {
        get
        {
            return new Vector2((int)player.transform.position.x, (int)player.transform.position.z);
        }
    }

    public Vector2 targetTile;

    public const int farmRows = 10;
    public const int farmColumns = 10;
    public FarmSquare[,] farm;

    // Use this for initialization
    void Start () {
        farm = new FarmSquare[farmRows, farmColumns];
        for (int y = 0; y < farmColumns; y++)
        {
            for (int x = 0; x < farmRows; x++)
            {
                farm[x,y] = (FarmSquare)GameObject.Instantiate(squarePrefab, new Vector3(x, 0, y), Quaternion.identity);
                farm[x, y].name = "FarmSquare " + x + "," + y;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        targetTile = playerTargetTile;

        if (Input.GetButtonDown("Fire1"))
        {
            if (playerTargetTile.x >= 0 && playerTargetTile.x <= farmRows - 1 && playerTargetTile.y >= 0 && playerTargetTile.y <= farmRows - 1)
            {
                farm[(int)playerTargetTile.x, (int)playerTargetTile.y].Interact(player.equipedTool);
            }
        }
	}
}
