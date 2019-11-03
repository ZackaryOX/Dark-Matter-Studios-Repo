using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private static int Objects = 0;
    //Contstructor
    public Entity(GameObject thisobject)
    {
        this.ThisObject = thisobject;
        this.Trans = thisobject.transform;
        Objects++;
        this.ID = Objects - 1;

    }
    //Public
    public int GetID()
    {
        return this.ID;
    }
    public Vector3 GetPosition()
    {

        return this.Trans.position;
    }
    public Vector3 GetRotationEuler()
    {
        return this.Trans.eulerAngles;
    }
    public Quaternion GetRotationQuat()
    {
        return this.Trans.rotation;
    }
    public void SetPosition(Vector3 temp)
    {
        this.Trans.position = temp;
    }
    void SetRotationEuler(Vector3 temp)
    {
        this.Trans.eulerAngles = temp;
    }
    void SetRotationQuat(Quaternion temp)
    {
        
    }

    public virtual void Update()
    {

    }



    //Private
    private GameObject ThisObject;
    private int ID = 0;
    private Transform Trans;

}