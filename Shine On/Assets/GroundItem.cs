using UnityEngine;
using System.Collections;

public class GroundItem : MonoBehaviour {

    SpriteRenderer render;

    public TilePosition position;
    public InventoryItem item = new InventoryItem();

	// Use this for initialization
	void Start () {
        render = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (item.identity == null)
        {
            Destroy(gameObject);
        }
        else
        {
            render.sprite = item.icon;
        }
        transform.position = new Vector3(position.x, 0, position.y);
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
