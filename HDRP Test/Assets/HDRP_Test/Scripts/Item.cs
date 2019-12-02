using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using System.IO.Ports;

public class Item : MonoBehaviour
{
    public Sprite ItemImage;
    public PickUp ThisItem;

    SerialPort _In = new SerialPort("COM3", 9600);

    public bool _Arduino;
    bool _pickUp;

    // Start is called before the first frame update
    void Awake()
    {
        ThisItem = new PickUp(gameObject, ItemImage);

        if(_Arduino == true)
        {
            _In.Open();
           _In.ReadTimeout = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        ThisItem.SetCanBePicked(true);
        if (GetComponent<mouseHovor>().mouseOver == true && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(ThisItem.GetPosition());
            Player.AllPlayers[0].AddItemToInventory(ThisItem.GetName());
            transform.parent = null;
            GetComponent<MeshCollider>().isTrigger = true;
            GetComponent<mouseHovor>().enabled = false;
            GetComponent<mouseHovor>().mouseOver = false;
            _pickUp = true;
        }
        if (_pickUp == true && _Arduino == true && ItemImage.name == "flashlight")
        {
            try
            {
                if (_In.IsOpen)
                {
                    string input = _In.ReadLine();
                    _In.ReadTimeout = 105;

                    if (input.Equals("ON"))
                    {
                        gameObject.GetComponent<Light>().enabled = false;
                        Debug.Log("false");
                    }
                    else if (input.Equals("OFF"))
                    {
                        gameObject.GetComponent<Light>().enabled = true;
                        Debug.Log("true");
                    }
                }

            }
            catch (System.Exception)
            {
                Debug.Log("Arduino Is Not Open, please uncheck _Arduino under Item.cs");
            }
        }
    }
}
