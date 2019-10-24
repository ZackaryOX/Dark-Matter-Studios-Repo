using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private CharacterController _controller;
    private Transform _transform;
    private Animator _animator;
    //Constructor 
    public PlayerInput(GameObject thisobject)
    {
        _controller = thisobject.GetComponent<CharacterController>();
        _transform = thisobject.GetComponent<Transform>();
        _animator = thisobject.GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }




    //For Walking
    private float Speed = 10;
    private float StartIncrement = 10;
    private float StopIncrement = 5;
    private float VerticalVelocity = 0;
    private float HorizontalVelocity = 0;

    private float MaxVelocity = 1;
    private float MinVelocity = -1;

    //For Jumping
    private float YVelocity = 0;
    private float JumpVelocity = 5;
    private float FallMultiplier = 1.2f;
    private bool IsJumping = false;
    private bool Adjusted = false;
    private int Jump = 0;

    public float JumpMult = 2.5f;
    public float lowJumpMult = 2f;
    //For Gravity
    private Vector3 BaseGravityForce = new Vector3(0, -9.8f, 0);
    private Vector3 AdjustedGravityForce = new Vector3(0, -9.8f, 0);

    //For Camera
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Vector3 RotateCamera()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        return new Vector3(pitch, yaw, 0.0f);
    }

    private void ComputeJump()
    {
        int TempJump = 0;
        //End of jump
        if (IsJumping && _controller.isGrounded)
        {
            YVelocity = 0;
            IsJumping = false;
            Adjusted = false;
            AdjustedGravityForce = BaseGravityForce;
        }
        //During jump
        else if (YVelocity > 0 && IsJumping)
        {

            YVelocity += AdjustedGravityForce.y * Time.deltaTime;

            if (!Adjusted && (int)YVelocity == -(int)BaseGravityForce.y)
            {
                Adjusted = true;
                AdjustedGravityForce = BaseGravityForce * FallMultiplier;
            }
        }


        //If conditions are met and spacebar is pressed 
        if (_controller.isGrounded && !IsJumping)
        {

            TempJump += GetInput(KeyCode.Space);
        }

        //Was SpaceBar Pressed? If so initiate jump
        if (TempJump > 0)
        {
            IsJumping = true;
            YVelocity += (AdjustedGravityForce.y * -1) + JumpVelocity;
        }



    }
    private int GetInput(KeyCode val)
    {
        if (Input.GetKey(val))
        {
            return 1;
        }

        return 0;
    }
    private float CheckVelocity(float Velocity)
    {
        if (Velocity < 0.1f && Velocity > -0.1f && Velocity != 0.0f)
        {
            return 0;
        }
        return Velocity;
    }
    private float GradualMovement(int rootaxisval, float max, float min, float cur, float startinc, float stopinc)
    {



        if ((rootaxisval == 0.0f) && (cur != 0.0f))
        {
            if (cur > 0.0f)
            {
                return min / stopinc;
            }
            else if (cur < 0.0f)
            {
                return max / stopinc;
            }
            return 0.0f;
        }

        if ((cur > min) && (cur < max))
        {
            return rootaxisval / startinc;
        }

        return 0.0f;
    }



    public void Update()
    {
        int Vertical = 0;
        int Horizontal = 0;

        float CurrentSpeed = 0;
        Vector3 Tempjumpvec;

        Vertical += GetInput(KeyCode.W);
        Vertical -= GetInput(KeyCode.S);
        Horizontal += GetInput(KeyCode.D);
        Horizontal -= GetInput(KeyCode.A);



        ComputeJump();


        Vector3 MouseRotation = _transform.eulerAngles = RotateCamera();


        HorizontalVelocity += GradualMovement(Horizontal, MaxVelocity, MinVelocity, HorizontalVelocity, StartIncrement, StopIncrement);
        VerticalVelocity += GradualMovement(Vertical, MaxVelocity, MinVelocity, VerticalVelocity, StartIncrement, StopIncrement);


        if (Vertical != 0 && Horizontal != 0)
        {
            CurrentSpeed = Speed / 2;

        }
        else
        {
            CurrentSpeed = Speed;
        }


        HorizontalVelocity = CheckVelocity(HorizontalVelocity);
        VerticalVelocity = CheckVelocity(VerticalVelocity);

        if (HorizontalVelocity != 0 || VerticalVelocity != 0)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        Vector3 VerticalMovement = _transform.forward * VerticalVelocity;
        Vector3 HorizontalMovement = _transform.right * HorizontalVelocity;





        Tempjumpvec = Vector3.up * YVelocity * Time.deltaTime;




        Vector3 MovementFinal = (HorizontalMovement + VerticalMovement) * CurrentSpeed * Time.deltaTime;
        MovementFinal.y = 0;
        Vector3 GravityFinal = AdjustedGravityForce * Time.deltaTime;


        Vector3 Final = MovementFinal + GravityFinal + Tempjumpvec;
        _controller.Move(Final);
    }
}