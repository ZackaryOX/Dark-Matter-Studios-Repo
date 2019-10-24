using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    //Constructor
    public Player(GameObject thisobject) : base(thisobject)
    {
        ThisInput = new PlayerInput(thisobject);
    }


    //Public
    public override void Update()
    {
        ThisInput.Update();
    }

    //Private
    private PlayerInput ThisInput;
    private Inventory ThisInventory;
}