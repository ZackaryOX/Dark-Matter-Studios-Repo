using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDoor : MonoBehaviour
{
    // Start is called before the first frame update
    Door ThisDoor;
    public GameObject Keyobject;
    private PickUp ThisKey;
    private void Awake()
    {
        ThisDoor = new Door(gameObject, true);
    }
    void Start()
    {
        ThisKey = PickUp.AllItems[Keyobject.name];
        ThisDoor.SetKey(ThisKey);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.E))
        {
            if(ThisDoor.GetIsOpened() == false)
            {
                ThisDoor.OpenDoor();
               
            }
            else if(ThisDoor.GetIsOpened() == true)
            {
                ThisDoor.CloseDoor();
            }
        }

        else if (ThisKey.GetPicked() == true && GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Player.AllPlayers[0].UseItemInInventory(ThisKey))
                ThisDoor.UnlockDoor();
        }
        ThisDoor.Update();
    }
}
