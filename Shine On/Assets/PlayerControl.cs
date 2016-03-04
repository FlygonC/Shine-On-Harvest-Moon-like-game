using UnityEngine;
using System.Collections;

public class PlayerControl : Entity {

    public static PlayerControl ThePlayer;

    [Header("Player:")]
    public Inventory InvRef;

    //public enum Heading { North = 0, East, South, West };
    // Heading facing;
    public enum Tool { Hands = 0, Holding, WaterCan, Hoe };
    //public Tool equipedTool = Tool.WaterCan;
    public bool holding
    {
        get
        {
            return InvRef.handHeldItem.identity != null;
        }
    }
    public bool interact = false;
    
    //public float walkSpeed = 0.1f;

    public float tempMoney = 0;

    public ItemObject[] startTools;

    public Vector2 interactionPoint
    {
        get
        {
            Vector2 ret = new Vector2();
            float reach = 0.8f;
            if (facing == Heading.East)
            {
                ret.x = transform.position.x + reach;
            }
            else if (facing == Heading.West)
            {
                ret.x = transform.position.x - reach;
            }
            else
            {
                ret.x = transform.position.x;
            }
            if (facing == Heading.North)
            {
                ret.y = transform.position.z + reach;
            }
            else if (facing == Heading.South)
            {
                ret.y = transform.position.z - reach;
            }
            else
            {
                ret.y = transform.position.z;
            }
            return ret;
        }
    }
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
    public InventoryItem heldItem
    {
        get
        {
            return InvRef.handHeldItem;
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


        /*foreach (ItemObject i in startTools)
        {
            if (i.name != "Seeds")
            {
                InvRef.AddItemToEmptySlot(i);
            }
            else
            {
                InvRef.AddItemToEmptySlot(i, 25);
            }
        }*/
        InvRef.AddItemToEmptySlot(startTools[0]);
        InvRef.AddItemToEmptySlot(startTools[1]);
        InvRef.AddItemToEmptySlot(startTools[2], 25);
        InvRef.AddItemToEmptySlot(startTools[3], 25);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (InvRef.handHeldItem.count > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = InvRef.handHeldItem.identity.icon;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            //equipedTool = Tool.Holding + (int)InvRef.handHeldItem.identity.tool;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            //equipedTool = Tool.Hands;
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
        Vector2 rawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (rawInput.magnitude > 1)
        {
            groundPosition += rawInput.normalized * walkSpeed * Time.deltaTime;
        } else
        {
            groundPosition += rawInput * walkSpeed * Time.deltaTime;
        }
        // Interaction
        interact = false;
        if (Input.GetButtonDown("Fire1"))
        {
            interact = true;
            if (InvRef.handHeldItem.identity == null)
            {
                GroundItem[] search = FindObjectsOfType<GroundItem>();
                foreach (GroundItem i in search)
                {
                    if (targetTile.Equals(i.position))
                    {
                        InvRef.PickUpItem(i.item.identity, i.item.count, i.item.data);
                        i.PickedUp();
                        interact = false;
                        break;
                    }
                }
            }
            
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
