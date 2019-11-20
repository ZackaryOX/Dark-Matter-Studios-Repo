using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DrawerObj;
    Drawer ThisDrawer;
    void Awake()
    {
        ThisDrawer = new Drawer(gameObject, DrawerObj);
    }

    // Update is called once per frame
    void Update()
    {
        ThisDrawer.Update();
    }
}