using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : Interactable
{
    public Drawer(GameObject Cabinet, GameObject drawer) : base(Cabinet)
    {
        ThisDrawer = drawer;
        ClosePosition = drawer.transform.position;
        OpenPosition = ClosePosition + new Vector3(1, 0, 0);
    }


    public GameObject GetDrawer()
    {
        return ThisDrawer;
    }

    public void Interact()
    {
        IsLerping = true;
    }

    public override void Update()
    {
        if (!IsLerping && Input.GetKeyDown(KeyCode.E) && ThisObject.GetComponent<mouseHovor>().mouseOver == true)
        {
            this.Interact();
        }
        if (IsLerping)
        {
            if (!Open)
            {
                LerpParam += 1 * Time.deltaTime;

                if (LerpParam >= 1)
                {
                    LerpParam = 1;
                    IsLerping = false;
                    Open = true;
                }
            }
            else
            {
                LerpParam -= 1 * Time.deltaTime;
                if (LerpParam <= 0)
                {
                    LerpParam = 0;
                    IsLerping = false;
                    Open = false;
                }
            }
            ThisDrawer.transform.position = Vector3.Lerp(ClosePosition, OpenPosition, LerpParam);
        }
    }




    private GameObject ThisDrawer;
    Vector3 OpenPosition;
    Vector3 ClosePosition;
    bool Open = false;
    bool IsLerping = false;
    float LerpParam = 0;
}