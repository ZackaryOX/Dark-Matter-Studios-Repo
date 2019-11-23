using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDoor : MonoBehaviour
{
    // Start is called before the first frame update
    Door ThisDoor;
    public GameObject Keyobject;
    PhotonView DoorView;
    private PickUp ThisKey;
    private void Awake()
    {

        ThisDoor = new Door(gameObject, true);
    }
    void Start()
    {
        ThisKey = PickUp.AllItems[Keyobject.name];
        ThisDoor.SetKey(ThisKey);
        DoorView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.E))
        {
            DoorView.RPC("DoorInteract", RpcTarget.AllBuffered, null); 
        }
        else if (ThisKey.GetPicked() == true && GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Player.AllPlayers[0].UseItemInInventory(ThisKey))
            {
                DoorView.RPC("DoorUnlock", RpcTarget.AllBuffered, null);
            }
                
        }
        ThisDoor.Update();
    }


    [PunRPC]
    void DoorInteract()
    {
        ThisDoor.Interact();
    }

    [PunRPC]
    void DoorUnlock()
    {
        ThisDoor.UnlockDoor();
    }
}
