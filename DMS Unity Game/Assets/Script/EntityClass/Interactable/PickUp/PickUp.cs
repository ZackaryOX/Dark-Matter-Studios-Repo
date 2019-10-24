using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUp : Interactable
{
    //Constructor:
    public PickUp(GameObject thisobject) : base(thisobject)
    {

    }

    //Public:
    public Image GetIcon()
    {
        return this.Icon;
    } 

    void SetImage(Image temp)
    {
        this.Icon = temp;
    }
    //Private:
    private Image Icon;
}
