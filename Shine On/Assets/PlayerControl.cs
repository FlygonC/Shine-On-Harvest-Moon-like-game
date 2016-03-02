using UnityEngine;
using System.Collections;

public class PlayerControl : Entity {

    public static PlayerControl ThePlayer;

    [Header("Player:")]
    public Inventory InvRef;

    //public enum Heading { North = 0, East, South, West };
    // Heading facing;
    public enum Tool { Hands = 0, Holding, WaterCan, Hoe, Seed };
    public Tool equipedTool = Tool.WaterCan;
    public bool interact = false;
    
    //public float walkSpeed = 0.1f;

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
        if (ThePlayer == null)
        {
            DontDestroyOnLoad(gameObject);
            ThePlayer = this;
        }
        else if (ThePlayer != this)
        {
            Destroy(gameObject);
        }


	    foreach (ItemObject i in startTools)
        {
            InvRef.AddItemToEmptySlot(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (InvRef.handHeldItem.count > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = InvRef.handHeldItem.identity.icon;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            equipedTool = Tool.Holding + (int)InvRef.handHeldItem.identity.tool;
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
        Vector3 rawInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (rawInput.magnitude > 1)
        {
            transform.position += rawInput.normalized * walkSpeed * Time.deltaTime;
        } else
        {
            transform.position += rawInput * walkSpeed * Time.deltaTime;
        }
        // Interaction
        interact = false;
        if (Input.GetButtonDown("Fire1"))
        {
            interact = true;
        }
        // Equipment
        /*if (Input.GetButtonDown("Fire2"))
        {
            InvRef.StoreHeldItem();
        }*/
        // Store Item
        if (Input.GetButtonDown("Fire3"))
        {
            InvRef.StoreHeldItem();
        }
	}
}
