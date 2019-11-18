using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private CharacterController _controller;
    private Transform _Headtransform;
    private Transform _transform;
    private Animator _animator;
    //Constructor 
    public PlayerInput(GameObject thisobject, GameObject tempHead)
    {
        _controller = thisobject.GetComponent<CharacterController>();
        _Headtransform = tempHead.GetComponent<Transform>();
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
    private float speedH = 4.0f;
    private float speedV = 4.0f;
    private float MaxPitch = 65.0f;
    private float MaxYaw = 90.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Vector3 RotateCamera()
    {
        yaw = 0;
        //float _InputX = Input.GetAxis("Mouse X");
        float ForYaw = Input.GetAxis("Mouse X");
        //ForYaw += _InputX > 0 && yaw + _InputX < MaxYaw ? _InputX : 0.0f;
        //ForYaw += _InputX < 0 && yaw - _InputX > -MaxYaw ? _InputX : 0.0f;

        float _InputY = Input.GetAxis("Mouse Y");
        float ForPitch = 0;
        ForPitch += _InputY > 0 && pitch + _InputY > -MaxPitch ? _InputY : 0.0f;
        ForPitch += _InputY < 0 && pitch + _InputY < MaxPitch ? _InputY : 0.0f;


        yaw += speedH * ForYaw;
        yaw = yaw > MaxYaw ? MaxYaw : yaw;
        yaw = yaw < -MaxYaw ? -MaxYaw : yaw;
        pitch -= speedV * ForPitch;
        pitch = pitch >= MaxPitch ? MaxPitch : pitch;
        pitch = pitch <= -MaxPitch ? -MaxPitch : pitch;

        return new Vector3(pitch, yaw + _transform.eulerAngles.y, 0.0f);
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
        if ((rootaxisval > 0 && cur < 0) || (rootaxisval < 0 && cur > 0))
        {
            if (cur > 0.0f)
            {
                return min / stopinc;
            }
            else if (cur < 0.0f)
            {
                return max / stopinc;
            }
        }
        //Start slowing down
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
        //Start Speeding up
        if ((cur > min) && (cur < max))
        {
            return rootaxisval / startinc;
        }

        return 0.0f;
    }



    public void Update(Stamina playerstam, PlayerState currentstate)
    {
        int Vertical = 0;
        int Horizontal = 0;
        int Running = 0;

        float CurrentSpeed = 0;
        Vector3 Tempjumpvec;

        if (currentstate.GetWalk())
        {
            Vertical += GetInput(KeyCode.W);
            Vertical -= GetInput(KeyCode.S);
            Horizontal += GetInput(KeyCode.D);
            Horizontal -= GetInput(KeyCode.A);
        }

        if(currentstate.GetRun())
        Running += GetInput(KeyCode.LeftShift);

        if(currentstate.GetJump())
        ComputeJump();


        Vector3 MouseRotation = _Headtransform.eulerAngles = RotateCamera();
        _transform.eulerAngles = new Vector3(0, MouseRotation.y, 0);

        HorizontalVelocity += GradualMovement(Horizontal, MaxVelocity, MinVelocity, HorizontalVelocity, StartIncrement, StopIncrement);
        VerticalVelocity += GradualMovement(Vertical, MaxVelocity, MinVelocity, VerticalVelocity, StartIncrement, StopIncrement);
        // Debug.Log("AFTER" + VerticalVelocity);

        //Reduces diagonal speed if A/D + W/S are pressed together
        CurrentSpeed = Vertical != 0 && Horizontal != 0 ? Speed / 2 : Speed;


        CurrentSpeed = Running > 0 ? CurrentSpeed * playerstam.DecreaseStam(Time.deltaTime) : CurrentSpeed;

        if(Running == 0)
        playerstam.IncreaseStam(Time.deltaTime, Mathf.Abs(Vertical) + Mathf.Abs(Horizontal));


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

        Vector3 VerticalMovement = _Headtransform.forward * VerticalVelocity;
        Vector3 HorizontalMovement = _Headtransform.right * HorizontalVelocity;





        Tempjumpvec = Vector3.up * YVelocity * Time.deltaTime;




        Vector3 MovementFinal = (HorizontalMovement + VerticalMovement) * CurrentSpeed * Time.deltaTime;
        MovementFinal.y = 0;
        Vector3 GravityFinal = AdjustedGravityForce * Time.deltaTime;


        Vector3 Final = MovementFinal + GravityFinal + Tempjumpvec;
        _controller.Move(Final);
    }
}