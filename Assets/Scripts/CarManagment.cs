using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagment : MonoBehaviour
{
    [SerializeField] private CarController[] cars;
    [SerializeField] private Speedometer speedometer;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalCarData.isFirstLoad)
        {   
            GlobalCarData.carIndex = 1;
            CarLoad(GlobalCarData.carIndex);
        }
        else 
        {   
            CarLoad(GlobalCarData.carIndex);
        }
    }
    public void CarLoad(int index)
    {
        foreach (CarController ccar in cars)
        {   
            if (ccar.carIndex == index)
            {
                ccar.gameObject.SetActive(true);
                ccar.tag = "Player";
                speedometer.rb = FindObjectOfType<CarController>().GetComponentInChildren<Rigidbody>();
            }
            else
            {
                ccar.gameObject.SetActive(false);
                ccar.tag = "NPC";
            }
        }
    }
}
