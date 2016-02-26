using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class InventoryItem
{
    public Item item;
    
    public int maxStack
    {
        get
        {
            return item.stackSize;
        }
    }
    public int count;

    public InventoryItem()
    {

    }
}
/* The InventoryItem class holds both an Item object and an integer for a number of items in the stack 
 * to allow items of the same type to be stacked in one slot.*/

public class Inventory : MonoBehaviour {

    public GameObject slotPrefab;
    public GameObject tooltipPrefab;
    private Text tooltipText;

    public List<GameObject> slots = new List<GameObject>();
    // #%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%
    public InventoryItem handHeldItem = new InventoryItem(); 
    public List<InventoryItem> items = new List<InventoryItem>();
    // #%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%

    //ItemDatabase _Data;

    public int numberOfSlots = 8;

	// Use this for initialization
	void Start () {

        //_Data = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        int slotIndexer = 0;
        //Create slots
        for (int x = 0; x < numberOfSlots; x++)
        {
            GameObject slot = (GameObject)Instantiate(slotPrefab);
            slot.transform.SetParent(this.transform, false);
            slot.GetComponent<RectTransform>().localPosition = new Vector3(x * 60, 0, 0);
            slot.name = "Slot " + x;
            slot.GetComponent<SlotScript>().slotNumber = slotIndexer;
            slotIndexer++;

            slots.Add(slot);
            items.Add(new InventoryItem());
        }
        //Create Tooltip
        GameObject tooltip = (GameObject)Instantiate(tooltipPrefab);
        tooltip.transform.SetParent(this.transform, false);
        tooltip.name = "Tooltip";
        tooltipText = tooltip.GetComponent<Text>();

        
	}

    //Use Update to handle Changes and interactions with items and slots
    //also to update the slots to be visually correct
    void Update()
    {
        //Check all items, if the count = 0, removes the item from inventory
        foreach (InventoryItem i in items)
        {
            if (i.count <= 0)
            {
                i.item = null;
                i.count = 0;
            }
        }
        //Update the slots
        //This could have been done on the slots themselves but I did it here because weirdness and 
        //I though it would be better to keep all the inventory interaction in one place.
        tooltipText.enabled = false;
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (items[i].item == null)
            {
                slots[i].GetComponent<SlotScript>().itemImage.enabled = false;
                slots[i].GetComponent<SlotScript>().itemCount.enabled = false;
            }
            else
            {
                slots[i].GetComponent<SlotScript>().itemImage.enabled = true;
                slots[i].GetComponent<SlotScript>().itemImage.sprite = items[i].item.icon;
                if (items[i].count > 1)
                {
                    slots[i].GetComponent<SlotScript>().itemCount.enabled = true;
                    slots[i].GetComponent<SlotScript>().itemCount.text = "" + items[i].count;
                }
                else if (items[i].count <= 1)
                {
                    slots[i].GetComponent<SlotScript>().itemCount.enabled = false;
                }
            }
            //When a slot is RightClicked, clicking is read by the slot
            if (slots[i].GetComponent<SlotScript>().rightClicked == true)
            {
                slots[i].GetComponent<SlotScript>().rightClicked = false;
                if (items[i].item != null && items[i].item.useable == true)
                {
                    items[i].item.UseItem(GameObject.FindGameObjectWithTag("Player"));
                    if (items[i].item.consumable)
                    {
                        items[i].count--;
                    }
                }
            }
            //when a slot is hovered over
            
            if (slots[i].GetComponent<SlotScript>().hover)
            {
                if (items[i].item != null)
                {
                    tooltipText.enabled = true;
                    tooltipText.gameObject.transform.position = Input.mousePosition;
                    tooltipText.gameObject.transform.position += new Vector3(0, -20, 0);
                    tooltipText.text = items[i].item.WriteTooltip();
                }
            }
        }
    }

    /*void AddItem(int a_id, int a_number)
    {
        for (int n = 1; n <= a_number; n++)
        {
            for (int i = 0; i < _Data.itemData.Count; i++)
            {
                //if id valid
                if (_Data.itemData[i].itemID == a_id)
                {
                    Item newItem = _Data.itemData[i];
                    //Check Stackability
                    if (_Data.itemData[i].stackSize > 1)
                    {
                        //if stackable, try to find a stack with room
                        if (AddItemToStack(newItem))
                        {
                            break;
                        }
                        else//add to new slot
                        {
                            AddItemToEmptySlot(newItem);
                        }
                    }
                    else//if not stackable
                    {
                        AddItemToEmptySlot(newItem);
                    }
                }
            }
        }
    }*/

    public void PickUpItem(Item _item)
    {
        if (handHeldItem.count == 0)
        {
            handHeldItem.item = _item;
            handHeldItem.count = 1;
        }
    }
    public bool StoreHeldItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == null)
            {
                items[i].item = handHeldItem.item;
                items[i].count = handHeldItem.count;

                handHeldItem.item = null;
                handHeldItem.count = 0;

                return true;
            }
        }
        return false;
    }

    public bool AddItemToEmptySlot(Item _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == null)
            {
                items[i].item = _item;
                items[i].count = 1;
                return true;
            }
        }
        return false;
    }

    public bool AddItemToStack(Item _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == _item)
            {
                if (items[i].count < items[i].maxStack)
                {
                    items[i].count += 1;
                    return true;
                }
            }
        }
        return false;
    }
}