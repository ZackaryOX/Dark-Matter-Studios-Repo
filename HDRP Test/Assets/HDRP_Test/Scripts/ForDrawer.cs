using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject OutterDrawer;
    Drawer ThisDrawer;
    PhotonView DrawerView;
    void Awake()
    {
        Debug.Log("creating drawer");
        ThisDrawer = new Drawer(OutterDrawer, gameObject);
    }
    private void Start()
    {
        DrawerView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {


        ThisDrawer.Update();
    }

    void OnMouseOver()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            ThisDrawer.Interact();
            //DrawerView.RPC("DrawerInteract", RpcTarget.AllBuffered, null);
        }

    }

    [PunRPC]
    public void DrawerInteract()
    {

        ThisDrawer.Interact();

    }
}