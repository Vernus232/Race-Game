using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Text text;
    
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        //FindCar(GlobalCarData.carIndex);
    }
    public void FindCar(int carIndex)
    {
        text = FindObjectOfType<Text>(CompareTag("Speedometer"));
        //rb = FindObjectOfType<CarController>(carIndex == GlobalCarData.carIndex).GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speedInKpH = rb.velocity.magnitude * 3.6f;
        text.text = speedInKpH.ToString("00 km/h");
    }
}
