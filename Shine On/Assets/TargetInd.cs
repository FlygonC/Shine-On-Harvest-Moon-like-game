using UnityEngine;
using System.Collections;

public class TargetInd : MonoBehaviour {

    public PlayerControl player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.targetTile.x, 0.02f, player.targetTile.y);
	}
}
