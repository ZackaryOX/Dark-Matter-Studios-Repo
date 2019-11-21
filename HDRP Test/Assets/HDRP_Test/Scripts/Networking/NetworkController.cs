using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    //From Zack: If you need any help regarding the networking plugin, use these links
    //DOCUMENTATION: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
    //SCRIPTING API: https://doc-api.photonengine.com/en/pun/v2/index.html


    public static NetworkController ThisLobby;
    private static bool RoomCreated = false;
    RoomOptions roomOps;
    RoomInfo ThisRoom;
    private static bool ConnectedToMaster = false;
    // Start is called before the first frame update
    public GameObject RoundStart;
    public GameObject RoundCancel;
    private string roomname = "Room1";
    void Start()
    {
        ThisLobby = this;
        PhotonNetwork.ConnectUsingSettings();//connects to photons master server
        roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxplayers };
    }

    public override void OnConnectedToMaster()
    {
        ConnectedToMaster = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("We are now connected to " + PhotonNetwork.CloudRegion + " server!");
        RoundStart.SetActive(true);
        RoundCancel.SetActive(false);
    }

    public void OnRoundStart()
    {

        RoundStart.SetActive(false);
        RoundCancel.SetActive(true);
        PhotonNetwork.JoinRoom(roomname);
    }
    public void OnRoundCancel()
    {
        RoundStart.SetActive(true);
        RoundCancel.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
    private void CreateRoom(int roomName)
    {
        Debug.Log("Room created");
        PhotonNetwork.CreateRoom(roomname, roomOps);
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("joining failed, creating new room");
        if (ConnectedToMaster)
        {
            RoomCreated = true;
            CreateRoom(1);
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("ROOM JOINED");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.J))
        {
            OnRoundStart();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnRoundCancel();
        }
    }
}
