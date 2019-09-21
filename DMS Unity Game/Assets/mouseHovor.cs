using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHovor : MonoBehaviour
{
    Vector4 defaultColor;
    Vector4 newColor = new Vector4(124.0f, 252.0f, 0.0f, 1.0f);
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.GetVector("_Color");
    }

    void OnMouseOver()
    {

     GetComponent<Renderer>().material.SetVector("_Color", newColor);
        
    }



    void OnMouseExit()
    {
        
            GetComponent<Renderer>().material.SetVector("_Color", defaultColor);
        
    }
  
       
 
}
