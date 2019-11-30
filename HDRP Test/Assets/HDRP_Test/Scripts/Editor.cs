using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Editor : MonoBehaviour
{
    Player ThisPlayer;
    public Canvas localGUI;
    public GameObject head;
    public GameObject MyCamera;
    public Image defaultIcon;
    public Image selectedIcon;
    public Image emptyItem;
    public Image StaminaBar;
    public Image HealthBar;
    public Image SanityBar;
    public Text SlotNumber;
    PlayerInventory hotbar;
    StatObserver Player1Stats;
    ScoreObserver Player1Score;

    int layerMask = 1 << 8;

    private InputManager input;
    private PausedState PauseMenu;
    private PlayerState OldState;
    public AudioManager ThisAudioManager;
    public GameObject TestBox;
    public GameObject UIElements;

    [FMODUnity.EventRef]
    public string[] SFXEventNames;

    [FMODUnity.EventRef]
    public string[] MusicEventNames;

    void Awake()
    {
        PauseMenu = new PausedState();
        PlayerAwake();
        Application.targetFrameRate = 60;
        ThisAudioManager = new AudioManager(SFXEventNames, MusicEventNames, head);
    }

    void Update()
    {

    }

    void LateUpdate()
    {


        ThisPlayer.Update();
        hotbar.Update();
        MyCamera.SetActive(true);
        localGUI.gameObject.SetActive(true);
        Vector3 Data = Player1Stats.GetData();
        StaminaBar.transform.localScale = new Vector3(Data.y / 100, 1, 1);
        HealthBar.transform.localScale = new Vector3(Data.x / 100, 1, 1);
        SanityBar.transform.localScale = new Vector3(Data.z / 100, 1, 1);
        ThisAudioManager.Update(100 - Data.z);


        if (Input.GetKeyDown(KeyCode.N))
        {
            //float tempforward = Vector3.Angle(head.transform.forward, TestBox.transform.position - head.transform.position);
            //Vector3 forup = new Vector3(TestBox.transform.position.x, 0, TestBox.transform.position.z);
            //Vector3 forupdeduction = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);
            //float tempup = Vector3.Angle(head.transform.up, forup - forupdeduction);
            //Debug.Log("Angle forward is: "+ tempforward);
            //Debug.Log("Angle up is: " + tempup);

            // Bit shift the index of the layer (8) to get a bit mask
            

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            

            

            // Does the ray intersect any objects excluding the player layer

        }

        

        if(Vector3.Angle(head.transform.forward, TestBox.transform.position - head.transform.position) <= 45)
        {
            RaycastHit hit;
            Vector3 Direction = ThisPlayer.GetObject().transform.position - TestBox.transform.position;
            if (!Physics.Raycast(TestBox.transform.position, Direction, out hit,
            Vector3.Distance(TestBox.transform.position, ThisPlayer.GetPosition()), layerMask))
            {
                Debug.Log("LOOKING AND CAN SEE");
            }
            else
            {
                Debug.Log("LOOKING AND CANT SEE");
            }
            
        }
        else
        {
            Debug.Log("NOT LOOKING");
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
    
    void PlayerAwake()
    {
        layerMask = ~layerMask;
        input = new InputManager();
        hotbar = new PlayerInventory(defaultIcon, selectedIcon, emptyItem, SlotNumber);
        ThisPlayer = new Player(gameObject, head, hotbar, false, input);
        Player1Stats = new StatObserver(ThisPlayer);
        Player1Score = new ScoreObserver(ThisPlayer);
        /*FOR TUTORIAL:*/ //Player1.SetState(new TeachWalkState());
                          /*FOR EDITING:*/
        ThisPlayer.SetState(new TeachPickupState());
    }
    void SetPosition()
    {
        Debug.Log("THIS IS HOW MANY PLAYERS: " + Player.AllPlayers.Count + " FROM PLAYER " + ThisPlayer.GetName());
        //Player1.SetPosition(GameObject.Find("Spawn0").transform.position);
    }
}