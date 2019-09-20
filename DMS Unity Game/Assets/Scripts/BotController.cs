using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float gravity = 9.8f;

    float acceleration = 1.0f;
    float curspeed = 0.0f;



    CharacterController controller;
    Rigidbody ThisRigidBody;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        anim = GetComponent<Animator> ();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        float walkspeed = 1.0f;
        float[] angles = { -1, -1, -1, -1};
        float finalangle = 0;
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDir.z += walkspeed;
                angles[0] = 0;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDir.z -= walkspeed;
                angles[1] = 180;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDir.x -= walkspeed;
                angles[2] = 270;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDir.x += walkspeed;
                angles[3] = 90;
            }

        }

        int amountofangles = 0;
        for (int i = 0; i < 4; i++)
        {
            if(angles[i] != -1)
            {
                amountofangles++;
                finalangle += angles[i];
            }
        }
        finalangle /= amountofangles;
        Vector3 Rotationvec = new Vector3(0.0f, finalangle, 0.0f);
        Vector3 newDirWithSpeed = moveDir * speed;
        newDirWithSpeed.y -= gravity;
        if( amountofangles > 0 && controller.transform.rotation.y != finalangle)
        {
            controller.transform.Rotate(-controller.transform.eulerAngles);
            controller.transform.Rotate(Rotationvec);
        }
        
       
        controller.Move(newDirWithSpeed * Time.deltaTime);
    }
}
