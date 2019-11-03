using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory 
{

    //Constructor:
    public Inventory()
    {

    }
    //Public:
 
    public void PickupItem(PickUp item)
    {
        if (Items.Count < 10)
            Items.Add(item);
    }
    public void Update()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] != null)
                Icons[i].GetComponent<Image>().sprite = Items[i].GetIcon().sprite;
        }
    }

    //Private:
    private List<PickUp> Items = new List<PickUp>() { };
    private List<Image> Icons = new List<Image>() { };
}
