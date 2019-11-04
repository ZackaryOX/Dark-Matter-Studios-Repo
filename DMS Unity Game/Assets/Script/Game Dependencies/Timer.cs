﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text GameTime;
    public static float ElapsedTime = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;

        int Minute = (int)ElapsedTime / 60;
        int Second = (int)ElapsedTime - (Minute * 60);

        string ForMins;
        string ForSecs;
        if(Minute < 10)
        {
            ForMins = "0"+ Minute.ToString();
        }
        else
        {
            ForMins = Minute.ToString();
        }

        if (Second < 10)
        {
            ForSecs = "0" + Second.ToString();
        }
        else
        {
            ForSecs = Second.ToString();
        }

        GameTime.text = "Time: " + ForMins + ":" + ForSecs;
    }
}