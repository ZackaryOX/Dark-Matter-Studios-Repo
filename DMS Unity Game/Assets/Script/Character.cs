using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Character : MonoBehaviour
{
    Player test;
    public GameObject head;
    public Image defaultIcon;
    public Image selectedIcon;
    public Image emptyItem;
    Inventory hotbar;

    void Start()
    {
        test = new Player(gameObject, head);
        hotbar = new Inventory(defaultIcon, selectedIcon, emptyItem);
    }

    void Update()
    {
        hotbar.Update();
    }

    void LateUpdate()
    {
        test.Update();
    }

}