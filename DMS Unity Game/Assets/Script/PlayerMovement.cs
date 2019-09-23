using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed = 5;
    private Vector3 moveDirection;
    public bool CanMove = true;

    Vector2 rotation = new Vector2(0, 0);
    public float sensitivity = 1.5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (CanMove == true)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            player.transform.eulerAngles = (Vector2)rotation * sensitivity;

            if (Input.GetKey("w"))
            {
                moveDirection = player.transform.rotation * Vector3.forward;
            }
            if (Input.GetKey("s"))
            {
                moveDirection = player.transform.rotation * -Vector3.forward;
            }
            if (Input.GetKey("d"))
            {
                moveDirection = player.transform.rotation * Vector3.right;
            }
            if (Input.GetKey("a"))
            {
                moveDirection = player.transform.rotation * -Vector3.right;
            }

            GetComponent<Rigidbody>().velocity = moveDirection * moveSpeed;
            moveDirection = new Vector3(0, 0, 0);
            player.transform.position = new Vector3(player.position.x, 3.0f, player.position.z);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
