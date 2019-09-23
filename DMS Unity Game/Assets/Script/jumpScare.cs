using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScare : MonoBehaviour
{
 
    bool mouseOver = false;
    bool switchBool = false;
    public PlayerMovement PMove;
    public lightFlicker LFlick;
    public GameObject lookingAt;
    public GameObject flashLight;
    public GameObject audio;
    public AudioSource audio2;
    public Material ghostmaterial;
    public Material mapMaterial;
    public Shader TVshader;
    public GameObject Ghost;
    public GameObject Player;
    void Awake()
    {

        TVshader = Shader.Find("Custom/TVFlicker");

        // rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (mouseOver == true)
        {
            flashLight.SetActive(false);
            
            if (switchBool == false)
            {
                Ghost.transform.position = new Vector3(6.0f,0.5f,24.2f);
                Ghost.transform.rotation = new Quaternion(0.0f, -200.6f, 0.0f, 1);
                Player.transform.position = new Vector3(4.6f, 2.0f, 22.304f);
                Player.transform.Rotate(-Player.transform.rotation.eulerAngles);
                Player.transform.Rotate(0,-67.0f, 0);
                audio.GetComponent<AudioSource>().Play();
                switchBool = true;
                mapMaterial.shader = TVshader;
                Shader standard = Shader.Find("Standard");
                ghostmaterial.shader = standard;
                PMove.CanMove = false;
                PMove.moveSpeed = 0;
                LFlick.StopFlicker = true;
                audio2.PlayDelayed(1);
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
    void OnDestroy()
    {
        Shader standard = Shader.Find("Standard");
        switchBool = false;
        mapMaterial.shader = standard;
        ghostmaterial.shader = Shader.Find("Outlined/Silhouetted Diffuse");
        Ghost.transform.position = new Vector3(17.7f, 0.4f, -0.7f);
        Ghost.transform.rotation = new Quaternion(0.0f, -66.8f, 0, 1);
    }
}
