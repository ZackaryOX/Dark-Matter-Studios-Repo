using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image menu;
    public Image keybinds;

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menu.gameObject.SetActive(false);
        keybinds.gameObject.SetActive(false);
        InputManager.ToggleKeys();
    }

    public void Keybinds()
    {
        menu.gameObject.SetActive(false);
        keybinds.gameObject.SetActive(true);
    }

    public void Exit()
    {

    }

    public void Back()
    {
        menu.gameObject.SetActive(true);
        keybinds.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                menu.gameObject.SetActive(true);
                InputManager.ToggleKeys();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                menu.gameObject.SetActive(false);
                keybinds.gameObject.SetActive(false);
                InputManager.ToggleKeys();
            }
        }
    }
}
