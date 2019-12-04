using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class PluginTesterScript : MonoBehaviour
{
    
    Door Obj;
    Timer T;
    bool check = false;
    const string DLL_NAME = "Tutorial DLL";

    [DllImport(DLL_NAME)]
    private static extern void writeTime(float _Time);

    [DllImport(DLL_NAME)]
    private static extern float readTime();

    [DllImport(DLL_NAME)]
    private static extern void deleteLogs();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Door.AllDoors[GetComponent<ForDoor>().ThisDoor.GetName()].GetIsOpened() == true && check == false)
        {
            writeTime(T.ReturnTime());
            check = true;
            Debug.Log(T.ReturnTime());
        }

        if (Input.GetKey(KeyCode.N))
        {
            writeTime(T.ReturnTime());
        }

        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log(readTime());
        }


    }
}
