using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Dictionary<int, Player> AllPlayers = new Dictionary<int, Player>();
    static int Players = 0;
    //Constructor
    public Player(GameObject thisobject, GameObject temphead, PlayerInventory tempinv, bool isEditor, InputManager tempInput) : base(thisobject)
    {
        Head = temphead;
        
        ThisInput = new PlayerInput(thisobject, temphead, tempInput, tempinv);
        ThisStamina = new Stamina(100, 12.5f, 40.0f);
        ThisInventory = tempinv;
        Name = "Player" + Players.ToString();
        thisobject.name = Name;
        
        Health = 100;
        Sanity = 100;

        if (!isEditor)
        {
            PlayerNumber = Players;
            Players++;
            AllPlayers.Add(PlayerNumber, this);
        }
    }


    //Public
    public PlayerState GetState()
    {
        return Mystate;
    }
    public float GetHealth()
    {
        return Health;
    }
    public float GetSanity()
    {
        return Sanity;
    }
    public float GetStamina()
    {
        return this.ThisStamina.GetStamina();
    }

    public void SetHealth(float temp)
    {
        Health = temp;
    }
    public float GetScore()
    {
        return TutorialScore;
    }

    public void SetSanity(float temp)
    {
        Sanity = temp;
    }
    public override void Update()
    {
        ThisInput.Update(ThisStamina, Mystate);
        TutorialScore = Timer.ElapsedTime;



        foreach(KeyValuePair<int, PlayerObserver> entry in Observers)
        {
            entry.Value.Update();
        }
    }
    
    public void AddItemToInventory(string pickupname) {
        if (Mystate.GetPickup())
        {
            Debug.Log("picking up" + this.ThisInventory.PickupItem(PickUp.AllItems[pickupname]));
            
        }
    }
    public bool UseItemInInventory(PickUp tempitem)
    {
        return this.ThisInventory.UseItem(tempitem);
    }

    public void AttachObserver(PlayerObserver temp)
    {
        Observers.Add(temp.GetID(), temp);
    }

    public void SetState(PlayerState temp)
    {
        Mystate = temp;
    }

    public void AdvanceLevel()
    {
        Mystate.Advance(this);
    }
    public void DettachObserver(PlayerObserver temp)
    {
        Observers.Remove(temp.GetID());
    }


    //Private
    private Dictionary<int, PlayerObserver> Observers = new Dictionary<int, PlayerObserver>();
    private float Health;
    private float Sanity;
    PlayerState Mystate;
    private float TutorialScore = 0;
    private PlayerInput ThisInput;
    private PlayerInventory ThisInventory;
    private GameObject Head;
    private int PlayerNumber;
    private Stamina ThisStamina;
}