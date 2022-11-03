using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NitroCounter : MonoBehaviour
{
    private CarController nitroData;
    private Text counterText;
    private Slider counterSlider;
    // Start is called before the first frame update
    void Start()
    {
        nitroData = FindObjectOfType<CarController>();
        counterText = FindObjectOfType<Text>();
        counterSlider = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = nitroData.nitroValue.ToString("");
        counterSlider.maxValue = nitroData.nitroMaxValue;
        counterSlider.value = nitroData.nitroValue;
    }
}
