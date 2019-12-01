using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerTrapNode : TrapNode
{
    public GameObject StartPos;
    public GameObject EndPos;
    public GameObject Prefab;
    private mouseHovor MyMouse;
    public int LerpTime;
    CrawlerTrap Slot;
    // Start is called before the first frame update
    void Start()
    {
        MyMouse = GetComponent<mouseHovor>();
        Slot = new CrawlerTrap(Prefab, StartPos, EndPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.AllPlayers.Count > 0 && MyMouse.mouseOver && Input.GetKeyDown(KeyCode.E))
        {
            if (Ghost.AllGhosts[0].GetTrap().type == TrapType.CRAWLER)
                isActive = true;
        }
        if (isActive)
        {
            if (Player.AllPlayers.Count > 0 && Vector3.Distance(Player.AllPlayers[0].GetObject().transform.position, this.transform.position) < 2)
            {
                ActivateTrap();
                isActive = false;
            }
        }
        Slot.Update();
    }

    public override void ActivateTrap()
    {
        Slot.Initiate(LerpTime);
    }


}
