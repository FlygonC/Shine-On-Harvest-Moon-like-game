using UnityEngine;
using System.Collections;

public class PlayerControl : Entity {

    public Inventory InvRef;

    public enum Heading { North = 0, East, South, West };
    public Heading facing;
    public enum Tool { Hands = 0, Holding, WaterCan, Hoe, Seed };
    public Tool equipedTool = Tool.WaterCan;
    public float walkSpeed = 0.1f;

    public float tempMoney = 0;

    public ItemObject[] startTools;

    public TilePosition targetTile
    {
        get
        {
            TilePosition ret = new TilePosition();
            float reach = 0.8f;
            if (facing == Heading.East)
            {
                ret.x = (int)(transform.position.x + reach);
            }
            else if (facing == Heading.West)
            {
                ret.x = (int)(transform.position.x - reach);
            }
            else
            {
                ret.x = (int)transform.position.x;
            }
            if (facing == Heading.North)
            {
                ret.y = (int)(transform.position.z + reach);
            }
            else if (facing == Heading.South)
            {
                ret.y = (int)(transform.position.z - reach);
            }
            else
            {
                ret.y = (int)transform.position.z;
            }
            return ret;
        }
    }

	// Use this for initialization
	void Start () {
	    foreach (ItemObject i in startTools)
        {
            InvRef.AddItemToEmptySlot(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (InvRef.handHeldItem.count > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = InvRef.handHeldItem.item.icon;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            equipedTool = Tool.Holding + (int)InvRef.handHeldItem.item.tool;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            equipedTool = Tool.Hands;
        }

        transform.rotation = Quaternion.Euler(0.0f, (float)facing * 90.0f, 0.0f);
        // Face Direction of Axis
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.5f)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                facing = Heading.North;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                facing = Heading.South;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                facing = Heading.East;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                facing = Heading.West;
            }
        }
        // Move
        Vector3 rawMovement = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, 0, Input.GetAxis("Vertical") * walkSpeed);
        if (rawMovement.magnitude > 1)
        {
            transform.position += rawMovement.normalized;
        } else
        {
            transform.position += rawMovement;
        }
        // Equipment
        /*if (Input.GetButtonDown("Fire2"))
        {
            equipedTool++;
            if (equipedTool > Tool.Seed)
            {
                equipedTool = Tool.Hands;
            }
        }*/
        // Store Item
        if (Input.GetButtonDown("Fire3"))
        {
            InvRef.StoreHeldItem();
        }
	}
}
