using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NitroCounter : MonoBehaviour
{
    private CarController сarController;
    private Text text;
    private Slider slider;


    void Start()
    {
        сarController = FindObjectOfType<CarController>();
        text = FindObjectOfType<Text>();
        slider = FindObjectOfType<Slider>();
    }

    void Update()
    {
        text.text = сarController.nitroValue.ToString("");
        slider.maxValue = сarController.nitroMaxValue;
        slider.value = сarController.nitroValue;
    }
}
