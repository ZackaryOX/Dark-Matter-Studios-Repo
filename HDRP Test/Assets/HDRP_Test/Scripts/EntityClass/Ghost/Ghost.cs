using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Entity
{
    //Constructor
    public Ghost(GameObject thisobject, GameObject temphead, GhostInventory tempinv) : base(thisobject)
    {
        Head = temphead;

        ThisInput = new GhostInput(thisobject, temphead);
        ThisStamina = new Stamina(100, 12.5f, 40.0f);
        ThisInventory = tempinv;
        thisobject.name = "Ghost";
    }


    //Public
    public float GetStamina()
    {
        return this.ThisStamina.GetStamina();
    }
    
    public float GetScore()
    {
        return TutorialScore;
    }

    public override void Update()
    {
        ThisInput.Update(ThisStamina);
        TutorialScore = Timer.ElapsedTime;
        
    }
    

    //Private
    private float TutorialScore = 0;
    private GhostInput ThisInput;
    private GhostInventory ThisInventory;
    private GameObject Head;
    private Stamina ThisStamina;
}
