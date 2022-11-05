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

    private int indx;


    
    public void ExitMechanic()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void FieldEnable(int index)
    {
        inputField.gameObject.SetActive(true);
        if (index == 1)
        {
            inputField.text = car.nitroPower.ToString();
            indx = index;
        }
        if (index == 2)
        {
            inputField.text = car.nitroMaxValue.ToString();
            indx = index;
        }
        if (index == 3)
        {
            inputField.text = car.motorForce.ToString();
            indx = index;
        }
        if (index == 4)
        {
            inputField.text = car.breakForce.ToString();
            indx = index;
        }
    }
    public void FieldDisable()
    {
        if (indx == 1) 
        {
            car.nitroPower = int.Parse(inputField.text);
        }
        if (indx == 2) 
        {
            car.nitroMaxValue = int.Parse(inputField.text);
        }
        if (indx == 3) 
        {
            car.motorForce = int.Parse(inputField.text);
        }
        if (indx == 4) 
        {
            car.breakForce = int.Parse(inputField.text);
        }
        inputField.gameObject.SetActive(false);
    }
}
