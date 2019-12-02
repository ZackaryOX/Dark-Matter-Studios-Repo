using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is to access the Serial Port functions and Classes
using System.IO.Ports;

public class ArduinoScript : MonoBehaviour
{
    //This line will assignment a serial port to a variable, so you can access it later
    //Make sure "COM5" is the same port in the ArduinoIDE
    //115200 is the data rate in bits per second (baud), this should match the rate you send it at, default to 9600 i believe
    SerialPort ardIn = new SerialPort("COM3", 9600);

    public bool _ValueUnity;



    void Start()
    {
        //Open the serial port
        ardIn.Open();
        //Sets the number of milliseconds before timeout occurs when a read operation does not finish
        //If something goes wrong in your read operations, it will timeout after ___ amount of milliseconds
        ardIn.ReadTimeout = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the port is open
        if (ardIn.IsOpen)
        {
            // Try and run this code 
            try
            {
                //String to store our inputs from the arduino
                string input = ardIn.ReadLine();
                ardIn.ReadTimeout = 105;

                Debug.Log(input);
                //printing out the input, just checking
                //if(input.Equals("ON"))
                //{
                //    Debug.Log("false");
                //}

                //else if (input.Equals("OFF"))
                //{
                //    Debug.Log("true");
                //}
                
                
               
            }
            //If there are any erros run this code
            //With the system.exeption, will run the error that we get if there is one
            catch (System.Exception)
            {
                Debug.Log("Not Working");
            }
        }

    }
}
