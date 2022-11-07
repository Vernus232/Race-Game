using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MechanicScene : MonoBehaviour
{
    [SerializeField] private CarController car;

    public Button exitButton;

    public Button nitroMax;
    public Button nitroPower;
    public Button engineForce;
    public Button breakForce;
    
    public InputField inputField;

    private int disableIndex;
    public CarController[] cars;
    [HideInInspector] public CarController currentCar;

    private void Start()
    {
        GlobalCarData.isFirstLoad = false;
        CarSwitch(GlobalCarData.carIndex);
    }
    
    public void ExitMechanic()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void FieldEnable(int enableIndex)
    {
        inputField.gameObject.SetActive(true);
        if (enableIndex == 1)
        {
            inputField.text = car.nitroPower.ToString();
            disableIndex = enableIndex;
        }
        if (enableIndex == 2)
        {
            inputField.text = car.nitroMaxValue.ToString();
            disableIndex = enableIndex;
        }
        if (enableIndex == 3)
        {
            inputField.text = car.motorForce.ToString();
            disableIndex = enableIndex;
        }
        if (enableIndex == 4)
        {
            inputField.text = car.breakForce.ToString();
            disableIndex = enableIndex;
        }
    }
    public void FieldDisable()
    {
        if (disableIndex == 1) 
        {
            GlobalCarData.nitroPower = int.Parse(inputField.text);
        }
        if (disableIndex == 2) 
        {
            GlobalCarData.nitroMaxValue = int.Parse(inputField.text);
        }
        if (disableIndex == 3) 
        {
            GlobalCarData.motorForce = int.Parse(inputField.text);
        }
        if (disableIndex == 4) 
        {
            GlobalCarData.breakForce = int.Parse(inputField.text);
        }
        inputField.gameObject.SetActive(false);
    }


    public void NextCar()
    {
        GlobalCarData.carIndex += 1;
        if (GlobalCarData.carIndex > cars.Length)
        {
            GlobalCarData.carIndex = 1;
        }
        CarSwitch(GlobalCarData.carIndex);
        currentCar = FindObjectOfType<CarController>(CompareTag("Player"));
        {
        GlobalCarData.motorForce = currentCar.motorForce;
        GlobalCarData.breakForce = currentCar.breakForce;
        GlobalCarData.maxSteerAngle = currentCar.maxSteerAngle;
        GlobalCarData.nitroMaxValue = currentCar.nitroMaxValue;
        GlobalCarData.nitroPower = currentCar.nitroPower;
        GlobalCarData.nitroDecrease = currentCar.nitroDecrease;
        }
    }
    public void PastCar()
    {
        GlobalCarData.carIndex -= 1;
        if (GlobalCarData.carIndex <= 0)
        {
            GlobalCarData.carIndex = cars.Length;
        }
        
        CarSwitch(GlobalCarData.carIndex);
        currentCar = FindObjectOfType<CarController>(CompareTag("Player"));
        {
        GlobalCarData.motorForce = currentCar.motorForce;
        GlobalCarData.breakForce = currentCar.breakForce;
        GlobalCarData.maxSteerAngle = currentCar.maxSteerAngle;
        GlobalCarData.nitroMaxValue = currentCar.nitroMaxValue;
        GlobalCarData.nitroPower = currentCar.nitroPower;
        GlobalCarData.nitroDecrease = currentCar.nitroDecrease;
        }
    }
    public void CarSwitch(int index)
    {
        print(GlobalCarData.carIndex);
        foreach (CarController ccar in cars)
        {
            if (ccar.carIndex == index)
            {
                ccar.gameObject.SetActive(true);
            }
            else
            {
                ccar.gameObject.SetActive(false);
            }
        }
    }
    
    
}
