using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //Constructor
    public Player(GameObject thisobject, GameObject temphead) : base(thisobject)
    {
        Head = temphead;
        ThisInput = new PlayerInput(thisobject, temphead);

    }


    //Public
    public override void Update()
    {
        ThisInput.Update();
    }

    //Private
    private PlayerInput ThisInput;
    private Inventory ThisInventory;
    private GameObject Head;
}