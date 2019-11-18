using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTrap : Trap
{

    //CONSTRUCTOR:
    public DogTrap() : base()
    {
        this.AddRequirement(Requirements.Hallway);
    }
    //PUBLIC:
    public void SetStart(Vector3 temp)
    {
        StartPoint = temp;
    }
    public void SetEnd(Vector3 temp)
    {
        EndPoint = temp;
    }
    public Vector3 GetStart()
    {
        return StartPoint;
    }
    public Vector3 GetEnd()
    {
        return EndPoint;
    }
    public override void Initiate()
    {
        //RUN THE TRAP HERE
    }
    //PRIVATE:
    private Vector3 StartPoint;
    private Vector3 EndPoint;
}
