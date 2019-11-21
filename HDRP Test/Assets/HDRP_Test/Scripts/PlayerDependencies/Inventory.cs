using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory
{

    //Constructor:
    public Inventory(Image Slot, Image Selected, Image emptyIcon)
    {
        //Set defaut images
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
        if (item.getCanBePicked() == true)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Items[i] == null)
                {
                    item.SetPosition(new Vector3(-100, -100, -100));
                    item.SetPicked(true);
                    Items[i] = item;
                    //Change Hotbar sprite to item's sprite
                    ItemIcons[i].GetComponent<Image>().sprite = Items[i].GetIcon();
                    return true;
                }
            }
        }
        return false;
    }

    public bool UseItem(PickUp temp)
    {
        if (Items[selected] == temp)
        {
            Items[selected].SetActive(false);
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
        if (Items[selected] != null)
            Items[selected].SetPosition(new Vector3(-100, -100, -100));
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected = temp;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }

    public void SelectNext()
    {
        //Change previously selected to none
        if (Items[selected] != null)
            Items[selected].SetPosition(new Vector3(-100, -100, -100));
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected++;
        if (selected > 9)
            selected = 0;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }

    public void SelectPrevious()
    {
        //Change previously selected to none
        if (Items[selected] != null)
            Items[selected].SetPosition(new Vector3(-100, -100, -100));
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected--;
        if (selected < 0)
            selected = 9;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }

    public void Update()
    {
        //Scroll up
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SelectPrevious();
        }
        //Scroll down
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SelectNext();
        }

        if (Input.GetKey(KeyCode.Alpha1))
            Select(0);
        else if (Input.GetKey(KeyCode.Alpha2))
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

        if (Items[selected] != null)
        {
            Items[selected].SetPosition(GameObject.Find("mixamorig:RightHandIndex1").transform.position);
            Items[selected].SetRotationEuler(GameObject.Find("mixamorig:RightHandIndex1").transform.rotation.eulerAngles);
        }

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
