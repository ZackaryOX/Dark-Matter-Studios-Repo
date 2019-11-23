using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHovor : MonoBehaviour
{


    public Material NewMat;
    public GameObject Object;
    public GameObject lookingAt;
    private Vector3 origRotation;

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
    }

    float ReturnGridPos(float x)
    {
        float gridtolockto = 0.25f;

        float posToSet = x;

        if (posToSet > 0)
        {
            posToSet /= gridtolockto;
            posToSet += 0.5f;
            float roundedX = (int)posToSet;
            roundedX *= gridtolockto;
            posToSet = roundedX;
        }
        else if (posToSet < 0)
        {
            posToSet /= gridtolockto;
            posToSet -= 0.5f;
            float roundedX = (int)posToSet;
            roundedX *= gridtolockto;
            posToSet = roundedX;
        }

        return posToSet;
    }

    void Update()
    {
        lookingAt = GameObject.Find(rayFromCamera.lookingAt);


        if (mouseOver && !MovingStuff)
        {

            Rend.material = NewMat;
            if (Input.GetKey(KeyCode.E))
            {
                //MovingStuff = true;
                //ObjectMoving = lookingAt;

            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            MovingStuff = false;
            return;
        }


    }

    void OnMouseOver()
    {
        lookingAt = GameObject.Find(rayFromCamera.lookingAt);
        mouseOver = true;



    }



    void OnMouseExit()
    {

        mouseOver = false;
        if (!MovingStuff)
        {
            Rend.material = OldMat;
        }


    }

}
