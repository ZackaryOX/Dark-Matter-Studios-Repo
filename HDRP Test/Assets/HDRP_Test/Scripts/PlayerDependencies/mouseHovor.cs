using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHovor : MonoBehaviour
{


    public Material NewMat;
    public GameObject Object;
    public GameObject lookingAt;


    MeshRenderer Rend;
    Material OldMat;

    public bool mouseOver = false;
    public static bool MovingStuff = false;
    public static GameObject ObjectMoving;


    void Awake()
    {
        //ObjectMoving = this.gameObject;
        Rend = GetComponent<MeshRenderer>();
        OldMat = Rend.material;
        Object = gameObject;
    }

   

    void Update()
    {
        lookingAt = GameObject.Find(rayFromCamera.lookingAt);

        if (Object == lookingAt)
        {
            mouseOver = true;
        }
        else
        {
            mouseOver = false;
            if (!MovingStuff)
            {
                Rend.material = OldMat;
            }
        }


        if (mouseOver && !MovingStuff)
        {

            Rend.material = NewMat;

        }




    }


}
