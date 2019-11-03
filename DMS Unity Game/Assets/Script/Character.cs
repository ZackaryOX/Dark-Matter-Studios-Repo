using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    Player test;
    public GameObject head;

    void Start()
    {
        test = new Player(gameObject, head);
    }

    void Update()
    {


    }

    void LateUpdate()
    {
        test.Update();
    }

}