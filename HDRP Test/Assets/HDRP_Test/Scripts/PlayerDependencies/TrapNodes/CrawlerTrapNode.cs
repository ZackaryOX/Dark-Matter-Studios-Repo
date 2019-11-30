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
        if(MyMouse.mouseOver && Input.GetKeyDown(KeyCode.E))
        {
            
            Slot.Initiate(LerpTime);
        }
        Slot.Update();
    }

 
}
