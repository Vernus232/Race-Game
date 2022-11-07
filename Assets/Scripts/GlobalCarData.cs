using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalCarData
{
    //Index
    public static int carIndex;

    //Main
    public static float motorForce;
    public static float breakForce;
    public static float maxSteerAngle;

    //Nitro
    public static float nitroMaxValue;
    public static float nitroPower;
    public static float nitroDecrease;
    public static bool isFirstLoad = true;
}
