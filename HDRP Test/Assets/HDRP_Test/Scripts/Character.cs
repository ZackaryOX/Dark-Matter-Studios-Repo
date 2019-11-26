using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviour
{
    Player ThisPlayer;
    private PhotonView PV;
    public Canvas localGUI;
    public GameObject head;
    public GameObject MyCamera;
    public Image defaultIcon;
    public Image selectedIcon;
    public Image emptyItem;
    public Image StaminaBar;
    public Image HealthBar;
    public Image SanityBar;
    Inventory hotbar;
    StatObserver Player1Stats;
    ScoreObserver Player1Score;

    private PausedState PauseMenu;
    private PlayerState OldState;
    public AudioManager ThisAudioManager;
    public GameObject AudioMenu;
    public GameObject UIElements;

    [FMODUnity.EventRef]
    public string[] SFXEventNames;

    [FMODUnity.EventRef]
    public string[] MusicEventNames;

    void Awake()
    {
        PauseMenu = new PausedState();
        PV = GetComponent<PhotonView>();
        PlayerAwake();
        Application.targetFrameRate = 60;
        ThisAudioManager = new AudioManager(SFXEventNames, MusicEventNames, head);
    }

    void Update()
    {
               
    }

    void LateUpdate()
    {
        if (PV.IsMine )
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (OldState == null)
                {
                    OldState = ThisPlayer.GetState();
                    ThisPlayer.SetState(PauseMenu);
                    Cursor.lockState = CursorLockMode.None;
                    AudioMenu.SetActive(true);
                    UIElements.SetActive(false);
                }
                else
                {
                    ThisPlayer.SetState(OldState);
                    OldState = null;
                    Cursor.lockState = CursorLockMode.Locked;
                    AudioMenu.SetActive(false);
                    UIElements.SetActive(true);
                }
            }

            ThisPlayer.Update();
            hotbar.Update();
            MyCamera.SetActive(true);
            localGUI.gameObject.SetActive(true);
            Vector3 Data = Player1Stats.GetData();
            StaminaBar.transform.localScale = new Vector3(Data.y / 100, 1, 1);
            HealthBar.transform.localScale = new Vector3(Data.x / 100, 1, 1);
            SanityBar.transform.localScale = new Vector3(Data.z / 100, 1, 1);
            ThisAudioManager.Update(100 - Data.z);
        }
        
    }

    public void SetSFXVolume(float temp)
    {
        ThisAudioManager.SetSFXVolume(temp);
    }
    public void SetMusicVolume(float temp)
    {
        ThisAudioManager.SetMusicVolume(temp);
    }
    public void SetMasterVolume(float temp)
    {
        ThisAudioManager.SetMasterVolume(temp);
    }


    [PunRPC]
    void PlayerAwake()
    {
        hotbar = new Inventory(defaultIcon, selectedIcon, emptyItem);
        ThisPlayer = new Player(gameObject, head, hotbar, false);
        Player1Stats = new StatObserver(ThisPlayer);
        Player1Score = new ScoreObserver(ThisPlayer);
       /*FOR TUTORIAL:*/ //Player1.SetState(new TeachWalkState());
       /*FOR EDITING:*/  ThisPlayer.SetState(new TeachPickupState());
    }
    [PunRPC]
    void SetPosition()
    {
        Debug.Log("THIS IS HOW MANY PLAYERS: " + Player.AllPlayers.Count+  " FROM PLAYER " + ThisPlayer.GetName());
        //Player1.SetPosition(GameObject.Find("Spawn0").transform.position);
    }
}