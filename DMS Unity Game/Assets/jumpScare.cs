using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScare : MonoBehaviour
{
 
    bool mouseOver = false;
    bool switchBool = false;
    public GameObject lookingAt;
    public GameObject flashLight;
    public GameObject audio;
    void Awake()
    {
       
    }

    void Update()
    {
        if (mouseOver == true)
        {
            flashLight.SetActive(false);
            if (switchBool == false)
            {
                audio.GetComponent<AudioSource>().Play();
                switchBool = true;
            }
        }
    }

    void OnMouseEnter()
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
        
        //    switchBool = true;
        //}else{
        //    switchBool = false;
        //}

    }

}
