using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Editor : MonoBehaviour
{
    Player Player1;

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

    void Awake()
    {

        PlayerAwake();
        Application.targetFrameRate = 60;
    }

    void Update()
    {

    }

    void LateUpdate()
    {


        Player1.Update();
        hotbar.Update();
        MyCamera.SetActive(true);
        localGUI.gameObject.SetActive(true);
        Vector3 Data = Player1Stats.GetData();
        StaminaBar.transform.localScale = new Vector3(Data.y / 100, 1, 1);
        HealthBar.transform.localScale = new Vector3(Data.x / 100, 1, 1);
        SanityBar.transform.localScale = new Vector3(Data.z / 100, 1, 1);


    }



    void PlayerAwake()
    {
        hotbar = new Inventory(defaultIcon, selectedIcon, emptyItem);
        Player1 = new Player(gameObject, head, hotbar, true);
        Player1Stats = new StatObserver(Player1);
        Player1Score = new ScoreObserver(Player1);
        /*FOR TUTORIAL:*/ //Player1.SetState(new TeachWalkState());
                          /*FOR EDITING:*/
        Player1.SetState(new TeachPickupState());
    }

    void SetPosition()
    {
        Debug.Log("THIS IS HOW MANY PLAYERS: " + Player.AllPlayers.Count + " FROM PLAYER " + Player1.GetName());
        //Player1.SetPosition(GameObject.Find("Spawn0").transform.position);
    }
}