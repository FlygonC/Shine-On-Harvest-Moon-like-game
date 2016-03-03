using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public class InventoryItem
{
    public ItemObject identity;
    
    
    public int count;

    public float data;

    public Sprite icon
    {
        get
        {
            return identity.icon;
        }
    }
    
}
/* The InventoryItem class holds both an ItemObject and an integer for a number of items in the stack 
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
                //i.identity = null;
                //i.count = 0;
                ClearItem(i);
            }
        }
        //Update the slots
        //This could have been done on the slots themselves but I did it here because weirdness and 
        //I though it would be better to keep all the inventory interaction in one place.
        tooltipText.enabled = false;
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (items[i].identity == null)
            {
                slots[i].GetComponent<SlotScript>().itemImage.enabled = false;
                slots[i].GetComponent<SlotScript>().itemCount.enabled = false;
            }
            else
            {
                slots[i].GetComponent<SlotScript>().itemImage.enabled = true;
                slots[i].GetComponent<SlotScript>().itemImage.sprite = items[i].icon;
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
                if (items[i].identity != null /*&& items[i].item.useable == true*/)
                {
                    
                    TakeOutItem(i);
                }
            }
            //when a slot is hovered over
            
            if (slots[i].GetComponent<SlotScript>().hover)
            {
                if (items[i].identity != null)
                {
                    tooltipText.enabled = true;
                    tooltipText.gameObject.transform.position = Input.mousePosition;
                    tooltipText.gameObject.transform.position += new Vector3(0, -20, 0);
                    tooltipText.text = items[i].identity.name + " (" + items[i].data + ")";
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

    public void PickUpItem(ItemObject _item, int _count = 1, float _quality = 0)
    {
        if (handHeldItem.count == 0)
        {
            handHeldItem.identity = _item;
            handHeldItem.count = _count;
            handHeldItem.data = _quality;
        }
    }
    public bool StoreHeldItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].identity == null)
            {
                //items[i].identity = handHeldItem.identity;
                //items[i].count = handHeldItem.count;
                //items[i].data = handHeldItem.data;
                AddItemToEmptySlot(handHeldItem.identity, handHeldItem.count, handHeldItem.data);

                ClearItem(handHeldItem);

                return true;
            }
        }
        return false;
    }
    public void TakeOutItem(int _fromSlot)
    {
        if (handHeldItem.count == 0 && PlayerControl.ThePlayer.holding == false)
        {
            handHeldItem.identity = items[_fromSlot].identity;
            handHeldItem.count = items[_fromSlot].count;
            handHeldItem.data = items[_fromSlot].data;

            ClearItem(items[_fromSlot]);
        }
    }

    public bool AddItemToEmptySlot(ItemObject _item, int _count = 1, float _quality = 0)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].identity == null)
            {
                items[i].identity = _item;
                items[i].count = _count;
                items[i].data = _quality;
                return true;
            }
        }
        return false;
    }

    /*public bool AddItemToStack(ItemObject _item, int _count = 1, float _quality = 0)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].identity == _item)
            {
                if (items[i].count < items[i].maxStack)
                {
                    items[i].count += 1;
                    return true;
                }
            }
        }
        return false;
    }*/

    void ClearItem(InventoryItem _target)
    {
        _target.identity = null;
        _target.count = 0;
    }
    InventoryItem CopyItem(InventoryItem _target)
    {
        return _target;
    }
}