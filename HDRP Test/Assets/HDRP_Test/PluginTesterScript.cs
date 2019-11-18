using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class PluginTesterScript : MonoBehaviour
{

    public GameObject Obj;
    bool check = false;
    const string DLL_NAME = "Tutorial DLL";

    [DllImport(DLL_NAME)]
    private static extern void writeTime(float _Time);

    [DllImport(DLL_NAME)]
    private static extern float readTime();

    [DllImport(DLL_NAME)]
    private static extern void deleteLogs();

    // Update is called once per frame
    void Update()
    {
        


    }
}
