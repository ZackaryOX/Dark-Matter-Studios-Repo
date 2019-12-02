﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Dictionary<int, Player> AllPlayers = new Dictionary<int, Player>();
    static int Players = 0;
    //Constructor
    public Player(GameObject thisobject, GameObject temphead, PlayerInventory tempinv, bool isEditor, InputManager tempInput, GameObject handtarget) : base(thisobject)
    {
        Head = temphead;

        ThisInput = new PlayerInput(thisobject, temphead, tempInput, tempinv);
        ThisStamina = new Stamina(100, 12.5f, 40.0f);
        ThisInventory = tempinv;
        Name = "Player" + Players.ToString();
        thisobject.name = Name;
        SetDefaultHandTarget(handtarget);
        SetHandTarget(handtarget);
        Health = 100;
        Sanity = 100;

        if (!isEditor)
        {
            PlayerNumber = Players;
            Players++;
            AllPlayers.Add(PlayerNumber, this);
            Added = true;
        }
    }

    ~Player()
    {
        if(Added)
        {
            AllPlayers.Remove(PlayerNumber);
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
        ThisInput.Update(ThisStamina, Mystate, CurrentHandTarget);
        TutorialScore = Timer.ElapsedTime;

        if(GetSanity() < 100)
        {
            Sanity += SanityRegen * Time.deltaTime;
            if(Sanity > 100.0f)
            {
                Sanity = 100;
            }

            if(Health > 0.0f)
            {
                float Multiplier = 0;
                if(Sanity < 75)
                {
                    Multiplier = 1;
                }
                if (Sanity < 50)
                {
                    Multiplier = 2;
                }
                if(Sanity < 25)
                {
                    Multiplier = 3;
                }

                Health -= Multiplier * HealthDamage * Time.deltaTime;
            }

            GetObject().GetComponent<PhotonView>().RPC("UpdatePlayer", RpcTarget.OthersBuffered, GetSanity(), GetHealth());
        }

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
    public GameObject GetHead()
    {
        return Head;
    }
    public int GetPlayerNumber()
    {
        return this.PlayerNumber;
    }
    public void SetWalkSpeed(float temp)
    {
        ThisInput.SetWalkSpeed(temp);
    }
    public float GetWalkSpeed()
    {
        return ThisInput.GetWalkSpeed();
    }
    public float GetDefaultSpeed()
    {
        return ThisInput.GetDefaultSpeed();
    }
    public void SetHandTarget(GameObject newhand)
    {
        CurrentHandTarget = DefaultHandTarget;
    }
    public void SetDefaultHandTarget(GameObject newhand)
    {
        DefaultHandTarget = newhand;
    }
    public void PutHandOut()
    {
        if (ThisInventory.IsItemInHand())
        {
            Animator ThisAnim = this.GetObject().GetComponent<Animator>();
            Vector3 worldpos = new Vector3(0.75f, 0, 3);

            Vector2 mousePos = Input.mousePosition;
            worldpos.x = mousePos.x;
            worldpos.y = mousePos.y;
            Vector3 newVec = Camera.main.ScreenToWorldPoint(worldpos);

            ThisAnim.SetIKPosition(AvatarIKGoal.RightHand, newVec);
            ThisAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.42f);

            ThisAnim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(new Vector3(27.0f, -50.0f, -160.0f) + this.GetRotationEuler()));
            ThisAnim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        }
    }
    //Private
    private Dictionary<int, PlayerObserver> Observers = new Dictionary<int, PlayerObserver>();
    private float Health;
    private float Sanity;
    PlayerState Mystate;
    private GameObject DefaultHandTarget;
    private GameObject CurrentHandTarget;
    private float TutorialScore = 0;
    private PlayerInput ThisInput;
    private PlayerInventory ThisInventory;
    private GameObject Head;
    private int PlayerNumber;
    private bool Added = false;
    private Stamina ThisStamina;
    private float SanityRegen = 0.75f;
    private float HealthDamage = 0.5f;
}