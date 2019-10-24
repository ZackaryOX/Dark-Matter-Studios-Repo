using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    Player test;

    void Start()
    {
        test = new Player(gameObject);
    }

    void Update()
    {
        test.Update();

    }

}