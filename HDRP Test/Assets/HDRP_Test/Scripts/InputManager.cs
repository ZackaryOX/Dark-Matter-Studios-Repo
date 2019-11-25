using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static Dictionary<string, KeyCode> Keys = new Dictionary<string, KeyCode>();

    bool waitingForKey = false;
    private static bool lockKeys = false;
    string waitingKeyName;
    Text waitingKeyText;

    private void Start()
    {
        Keys["forward"] = KeyCode.W;
        Keys["backward"] = KeyCode.S;
        Keys["right"] = KeyCode.D;
        Keys["left"] = KeyCode.A;
        Keys["jump"] = KeyCode.Space;
        Keys["run"] = KeyCode.LeftShift;
        Keys["interact"] = KeyCode.E;
        Keys["use"] = KeyCode.Mouse0;

        Keys["one"] = KeyCode.Alpha1;
        Keys["two"] = KeyCode.Alpha2;
        Keys["three"] = KeyCode.Alpha3;
        Keys["four"] = KeyCode.Alpha4;
        Keys["five"] = KeyCode.Alpha5;
        Keys["six"] = KeyCode.Alpha6;
        Keys["seven"] = KeyCode.Alpha7;
        Keys["eight"] = KeyCode.Alpha8;
        Keys["nine"] = KeyCode.Alpha9;
        Keys["zero"] = KeyCode.Alpha0;
    }

    private void Update()
    {
        if (waitingForKey)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kc))
                    {
                        Keys[waitingKeyName] = kc;

                        waitingKeyText.text = kc.ToString();

                        waitingForKey = false;
                        break;
                    }
                }
            }
        }
    }

    public void RebindKey(string KeyName)
    {
        if (!waitingForKey)
            waitingKeyName = KeyName;

    }

    public void StoreText(Text KeyText)
    {
        if (!waitingForKey)
            waitingKeyText = KeyText;
        waitingForKey = true;
    }

    public static bool GetKey(string temp)
    {
        if (lockKeys)
            return false;
        return Input.GetKey(Keys[temp]);
    }

    public static bool GetKeyDown(string temp)
    {
        if (lockKeys)
            return false;
        return Input.GetKeyDown(Keys[temp]);
    }

    public static bool GetKeyUp(string temp)
    {
        if (lockKeys)
            return false;
        return Input.GetKeyUp(Keys[temp]);
    }

    public static float GetScrollWheel()
    {
        if (lockKeys)
            return 0;
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public static float GetMouseX()
    {
        if (lockKeys)
            return 0;
        return Input.GetAxis("Mouse X");
    }

    public static float GetMouseY()
    {
        if (lockKeys)
            return 0;
        return Input.GetAxis("Mouse Y");
    }

    public static void ToggleKeys()
    {
        lockKeys = !lockKeys;
    }
}
