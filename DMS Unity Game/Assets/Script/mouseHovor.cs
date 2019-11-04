using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHovor : MonoBehaviour
{
    Vector4 defaultColor;
    Vector4 newColor;
    bool mouseOver = false;
   // bool switchBool = false;
    public GameObject lookingAt;
    void Awake()
    {
        defaultColor = GetComponent<Renderer>().material.GetVector("_Color2");
        newColor = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
    }

    void Update()
    {
       if(mouseOver == true)
        {
            lookingAt.GetComponent<Renderer>().material.SetVector("_Color2", newColor);
        }
    }

    void OnMouseOver()
    {
        lookingAt = GameObject.Find(rayFromCamera.lookingAt);
        mouseOver = true;
        //if (mouseOver == true && switchBool == false)
        //{
            //GetComponent<Renderer>().material.SetVector("_Color2", newColor);
            //switchBool = false;
        //}else{
        //    switchBool = true;
        //}

    }



    void OnMouseExit()
    {
        mouseOver = false;
        //if (mouseOver == false && switchBool == true)
        //{
            lookingAt.GetComponent<Renderer>().material.SetVector("_Color2", defaultColor);
        //    switchBool = true;
        //}else{
        //    switchBool = false;
        //}
       
    }

}
