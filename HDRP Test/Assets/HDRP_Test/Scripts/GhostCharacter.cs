using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GhostCharacter : MonoBehaviour
{
    Ghost ThisPlayer;
    private PhotonView PV;
    public GameObject head;
    public GameObject MyCamera;
    public GameObject MyFBOCam;
    public Image defaultIcon;
    public Image selectedIcon;
    public Image emptyItem;
    public Text SlotNumber;
    public Image StaminaBar;
    public Image HealthBar;
    public GameObject Surfaces;
    public GameObject Joints;
    GhostInventory hotbar;
    GhostStatObserver Player1Stats;
    GhostScoreObserver Player1Score;

    private InputManager input;
    private PausedState PauseMenu;
    private PlayerState OldState;
    public AudioManager ThisAudioManager;

    public GameObject menu;
    public GameObject settings;
    public GameObject audiosettings;
    public GameObject keybinds;
    public GameObject confirmation;


    public Materialise TestAbility;

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


        ThisPlayer.AddRenderer(Surfaces.GetComponent<SkinnedMeshRenderer>());

        ThisPlayer.AddRenderer(Joints.GetComponent<SkinnedMeshRenderer>());
        ThisPlayer.SetTransparency(0.1f);

       
    }
    private void Start()
    {
        
    }
    void Update()
    {

    }

    void LateUpdate()
    {
        if(TestAbility == null && Player.AllPlayers.Count > 0)
        {
            float LookDamage = 2.0f;
            float AOEDamage = 2.0f;
            float AOERadius = 3.5f;
            float Cooldown = 5.0f;
            float ActiveTime = 5.0f;

            TestAbility = new Materialise(ThisPlayer, Player.AllPlayers[0], Cooldown, ActiveTime, AOERadius, AOEDamage, LookDamage);
        }
        if(Player.AllPlayers.Count == 0)
        {

        }
        else if (PV.IsMine)
        {
            if (input.GetKeyDown("escape"))
            {
                if (OldState == null)
                {
                    OldState = ThisPlayer.GetState();
                    ThisPlayer.SetState(PauseMenu);
                    Cursor.lockState = CursorLockMode.None;
                    menu.SetActive(true);
                }
                else
                {
                    Resume();
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                TestAbility.Activate();
            }
            ThisPlayer.Update();
            hotbar.Update();
            input.Update();
            MyCamera.SetActive(true);
            MyFBOCam.SetActive(true);
            Vector2 Data = Player1Stats.GetData();
            StaminaBar.transform.localScale = new Vector3(Data.y / 100, 1, 1);
            HealthBar.transform.localScale = new Vector3(Data.x / 100, 1, 1);
            TestAbility.Update(PV);

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
    public void RebindKey(string KeyName)
    {
        input.RebindKey(KeyName);
    }
    public void StoreText(Text KeyText)
    {
        input.StoreText(KeyText);
    }
    public void ResetKeys()
    {
        input.ResetKeys();
    }
    public void Resume()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        audiosettings.SetActive(false);
        keybinds.SetActive(false);
        confirmation.SetActive(false);
        ThisPlayer.SetState(OldState);
        OldState = null;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Settings()
    {
        settings.SetActive(true);
        menu.SetActive(false);
        audiosettings.SetActive(false);
        keybinds.SetActive(false);
    }
    public void Audio()
    {
        audiosettings.SetActive(true);
        settings.SetActive(false);
    }
    public void Keybinds()
    {
        keybinds.SetActive(true);
        settings.SetActive(false);
    }
    public void Exit()
    {
        menu.SetActive(false);
        confirmation.SetActive(true);
    }
    public void Menu()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        audiosettings.SetActive(false);
        keybinds.SetActive(false);
        confirmation.SetActive(false);
    }
    public void Leave()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }



    [PunRPC]
    void PlayerAwake()
    {
        input = new InputManager();
        hotbar = new GhostInventory(defaultIcon, selectedIcon, emptyItem, SlotNumber);
        MyCamera.SetActive(false);
        MyFBOCam.SetActive(false);
        ThisPlayer = new Ghost(gameObject, head, hotbar, false, input);
        Player1Stats = new GhostStatObserver(ThisPlayer);
        Player1Score = new GhostScoreObserver(ThisPlayer);
        /*FOR TUTORIAL:*///ThisPlayer.SetState(new TeachWalkState());
        /*FOR EDITING:*/ThisPlayer.SetState(new TeachPickupState());

    }
    [PunRPC]
    void SetPosition()
    {
        Debug.Log("THIS IS HOW MANY PLAYERS: " + Player.AllPlayers.Count + " FROM PLAYER " + ThisPlayer.GetName());
        //Player1.SetPosition(GameObject.Find("Spawn0").transform.position);
    }

    [PunRPC]
    public void SetCasterTransparency(float albedo)
    {
        Ghost.AllGhosts[0].SetTransparency(albedo);
    }

    [PunRPC]
    public void SetTargetSanity(float newSanity)
    {
         Player.AllPlayers[0].SetSanity(newSanity);
    }
}
