﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite ItemImage;
    public PickUp ThisItem;
    // Start is called before the first frame update
    void Awake()
    {

        ThisItem = new PickUp(gameObject, ItemImage);

    }

    // Update is called once per frame
    void Update()
    {
        ThisItem.SetCanBePicked(true);
        if (GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(ThisItem.GetPosition());
            Player.AllPlayers[0].AddItemToInventory(ThisItem.GetName());
            transform.parent = null;
            GetComponent<MeshCollider>().isTrigger = true;
            GetComponent<mouseHovor>().enabled = false;
        }
    }
}
