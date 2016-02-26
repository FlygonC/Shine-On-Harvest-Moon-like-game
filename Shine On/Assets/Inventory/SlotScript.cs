using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

    //public Item item;
    public Image itemImage;
    public Text itemCount;
    public int slotNumber;
    //Inventory _Inventory;

    public bool leftClicked = false;
    public bool rightClicked = false;
    public bool hover = false;

	// Use this for initialization
	void Start () {
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemCount = gameObject.transform.GetChild(1).GetComponent<Text>();
        //_Inventory = GetComponentInParent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        //Icon Update
        /*if (_Inventory.items[slotNumber].item != null)
        {
            //this.GetComponentInChildren<Image>().enabled = true;
            itemImage.enabled = true;
            itemImage.sprite = _Inventory.items[slotNumber].item.icon;

            if (_Inventory.items[slotNumber].item.stackSize > 1)
            {
                if (_Inventory.items[slotNumber].count > 1)
                {
                    itemCount.enabled = true;
                    itemCount.text = "" + _Inventory.items[slotNumber].count;
                }
                else
                {
                    itemCount.enabled = false;
                }
            }
        }
        else
        {
            itemImage.enabled = false;
            itemCount.enabled = false;
            //_Inventory.items[slotNumber].count = 0;
        }*/

	}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.name + " Clicked! " + eventData.button);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            leftClicked = true;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            rightClicked = true;
        //    _Inventory.items[slotNumber].item.UseItem(GameObject.FindGameObjectWithTag("Player"));
        //    _Inventory.items[slotNumber].count--;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hover");
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }
}
