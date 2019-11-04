using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina
{
    //CONSTRUCTOR:
    public Stamina(int stamina, float regen)
    {
        MaxStam = stamina;
    }
    //PUBLIC:

    //PRIVATE:

    private int MaxStam;
    private int CurrentStam;
    private int Regen;
}
