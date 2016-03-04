using UnityEngine;
using System.Collections;

public class DepositBox : MonoBehaviour {
    
    public TilePosition tilePos;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerControl.ThePlayer.interact && PlayerControl.ThePlayer.targetTile.Equals(tilePos))
        {
            if (PlayerControl.ThePlayer.heldItem.identity as Produce)
            {
                PlayerControl.ThePlayer.tempMoney += 12 * PlayerControl.ThePlayer.heldItem.data;
                PlayerControl.ThePlayer.heldItem.count--;
            }
        }
    }
}
