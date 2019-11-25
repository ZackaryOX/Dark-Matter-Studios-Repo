using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DrawerObj;
    Drawer ThisDrawer;
    PhotonView MasterView;
    void Awake()
    {
       
        ThisDrawer = new Drawer(gameObject, DrawerObj);
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MasterView)
        MasterView.RPC("UpdateDrawer", RpcTarget.All);
        else
        {
            MasterView = Player.AllPlayers[0].GetObject().GetComponent<PhotonView>();
        }
    }

    [PunRPC]
    void UpdateDrawer()
    {
        ThisDrawer.Update();
    }
}