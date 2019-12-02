using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerTrapNode : TrapNode
{
    public GameObject StartPos;
    public GameObject EndPos;
    public GameObject Prefab;
    private GhostmouseHover MyMouse;
    public int LerpTime;
    CrawlerTrap Slot;
    // Start is called before the first frame update
    void Start()
    {
        MyMouse = GetComponent<GhostmouseHover>();
        Slot = new CrawlerTrap(Prefab, StartPos, EndPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.AllPlayers.Count > 0 &&  Ghost.AllGhosts.Count > 0)
        {

            if ( Ghost.AllGhosts[0].GetTrap().type == TrapType.CRAWLER && MyMouse.mouseOver && Input.GetKeyDown(KeyCode.E))
            {

                isActive = true;

            }
                
        }

        if (isActive)
        {
            if (Player.AllPlayers.Count > 0 && Vector3.Distance(Player.AllPlayers[0].GetObject().transform.position, this.transform.position) < 2)
            {
                ActivateTrap();
                isActive = false;
            }
            
        }
        if (Slot != null)
        {
            Slot.Update();
        }
    }

    public override void ActivateTrap()
    {
        Slot.Initiate(LerpTime);
    }


}
