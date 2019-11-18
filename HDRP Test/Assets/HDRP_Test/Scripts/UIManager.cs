using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static int PlayerStateUI = 0;
    public Image SpaceToJump;
    public Image ShiftToRun;
    public Image EToInteract;
    public Text LeftClickToUse;

    // Start is called before the first frame update
    void Start()
    {
        SpaceToJump.gameObject.SetActive(false);
        ShiftToRun.gameObject.SetActive(false);
        EToInteract.gameObject.SetActive(false);
        LeftClickToUse.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStateUI == 1)
        {
            SpaceToJump.gameObject.SetActive(true);
            ShiftToRun.gameObject.SetActive(true);
        }
        else if (PlayerStateUI == 2)
        {
            EToInteract.gameObject.SetActive(true);
            LeftClickToUse.gameObject.SetActive(true);
        }
    }
}