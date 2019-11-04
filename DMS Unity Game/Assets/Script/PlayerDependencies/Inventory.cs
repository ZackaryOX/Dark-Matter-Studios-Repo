using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory
{

    //Constructor:
    public Inventory(Image Slot, Image Selected,Image emptyIcon)
    {
        //Set defauts
        noItemIcon = emptyIcon;
        notSelectedIcon = Slot;
        selectedIcon = Selected;
        //Create hotbar
        for (int i = 0; i < 10; i++)
        {
            //Create null item list
            Items.Add(null);
            //Create background slots
            Slots.Add(GameObject.Instantiate(Slot, Slot.transform.parent));
            Slots[i].transform.localPosition = new Vector3(-500 + 110 * i, -450, 0);
            //Create item slots
            ItemIcons.Add(GameObject.Instantiate(emptyIcon, emptyIcon.transform.parent));
            ItemIcons[i].transform.localPosition = new Vector3(-500 + 110 * i, -450, 0);
        }
        //Set selected slot to "Selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }

    //Public:
    public bool PickupItem(PickUp item)
    {
        for (int i = 0; i < 10; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                //Change Hotbar sprite to item's sprite
                ItemIcons[i].GetComponent<Image>().sprite = Items[i].GetIcon().sprite;
                return true;
            }
        }
        return false;
    }

    public bool UseItem(PickUp temp)
    {
         if (Items[selected] == temp)
         {
             Items[selected] = null;
            //Change image to none
             ItemIcons[selected].GetComponent<Image>().sprite = noItemIcon.sprite;
             return true;
         }
         else
             return false;
    }

    public void Select(int temp)
    {
        //Change previously selected to none
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected = temp;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }

    public void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
            Select(0);
        else if(Input.GetKey(KeyCode.Alpha2))
            Select(1);
        else if (Input.GetKey(KeyCode.Alpha3))
            Select(2);
        else if (Input.GetKey(KeyCode.Alpha4))
            Select(3);
        else if (Input.GetKey(KeyCode.Alpha5))
            Select(4);
        else if (Input.GetKey(KeyCode.Alpha6))
            Select(5);
        else if (Input.GetKey(KeyCode.Alpha7))
            Select(6);
        else if (Input.GetKey(KeyCode.Alpha8))
            Select(7);
        else if (Input.GetKey(KeyCode.Alpha9))
            Select(8);
        else if (Input.GetKey(KeyCode.Alpha0))
            Select(9);
    }

    //Private:
    private List<PickUp> Items = new List<PickUp>() { };
    
    private List<Image> Slots = new List<Image>() { };
    private Image selectedIcon;
    private Image notSelectedIcon;

    private List<Image> ItemIcons = new List<Image>() { };
    private Image noItemIcon;

    private int selected = 0;
}
