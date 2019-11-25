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

    public virtual void Select(int temp)
    {
    }

    public virtual void SelectNext()
    {
    }

    public virtual void SelectPrevious()
    {
    }

    public virtual void UpdateSelected()
    {
    }

    public void Update()
    {
        //Scroll up
        if (InputManager.GetScrollWheel() > 0f)
        {
            SelectPrevious();
        }
        //Scroll down
        else if (InputManager.GetScrollWheel() < 0f)
        {
            SelectNext();
        }

        if (InputManager.GetKey("one"))
            Select(0);
        else if (InputManager.GetKey("two"))
            Select(1);
        else if (InputManager.GetKey("three"))
            Select(2);
        else if (InputManager.GetKey("four"))
            Select(3);
        else if (InputManager.GetKey("five"))
            Select(4);
        else if (InputManager.GetKey("six"))
            Select(5);
        else if (InputManager.GetKey("seven"))
            Select(6);
        else if (InputManager.GetKey("eight"))
            Select(7);
        else if (InputManager.GetKey("nine"))
            Select(8);
        else if (InputManager.GetKey("zero"))
            Select(9);

        UpdateSelected();
    }

    //Private:

    protected List<Image> Slots = new List<Image>() { };
    protected Image selectedIcon;
    protected Image notSelectedIcon;

    protected List<Image> ItemIcons = new List<Image>() { };
    protected Image noItemIcon;

    protected int selected = 0;
}

public class PlayerInventory : Inventory
{
    public PlayerInventory(Image Slot, Image Selected, Image emptyIcon) : base(Slot, Selected, emptyIcon)
    {
        for (int i = 0; i < 10; i++)
            Items.Add(null);
    }

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
    public override void Select(int temp)
    {
        //Change previously selected to none
        if (Items[selected] != null)
            Items[selected].SetPosition(new Vector3(-100, -100, -100));
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected = temp;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }
    public override void SelectNext()
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
    public override void SelectPrevious()
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
    public override void UpdateSelected()
    {
        if (Items[selected] != null)
        {
            Items[selected].SetPosition(GameObject.Find("mixamorig:RightHandIndex1").transform.position);
            Items[selected].SetRotationEuler(GameObject.Find("mixamorig:RightHandIndex1").transform.rotation.eulerAngles);
        }
    }


    private List<PickUp> Items = new List<PickUp>() { };
}

public class GhostInventory : Inventory
{
    public GhostInventory(Image Slot, Image Selected, Image emptyIcon) : base(Slot, Selected, emptyIcon)
    {
        for (int i = 0; i < 10; i++)
            Traps.Add(null);
    }

    public override void Select(int temp)
    {
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected = temp;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }
    public override void SelectNext()
    {
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected++;
        if (selected > 9)
            selected = 0;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }
    public override void SelectPrevious()
    {
        Slots[selected].GetComponent<Image>().sprite = notSelectedIcon.sprite;
        selected--;
        if (selected < 0)
            selected = 9;
        //Change new selected to "selected" sprite
        Slots[selected].GetComponent<Image>().sprite = selectedIcon.sprite;
    }
    public Trap GetTrap()
    {
        return Traps[selected];
    }
    public void AddTrap(Trap temp)
    {
        for (int i = 0; i < 10; i++)
        {
            if (Traps[i] == null)
            {
                Traps[i] = temp;
                //ItemIcons[i].GetComponent<Image>().sprite = Items[i].GetIcon();
                break;
            }
        }
    }
    public bool PlaceTrap(TrapNode temp)
    {
        if(Traps[selected] != null)
        {
            //temp.Slot = Traps[selected];
            return true;
        }
        return false;
    }

    private List<Trap> Traps = new List<Trap>() { };
}