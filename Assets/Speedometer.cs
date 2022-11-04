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
    text = FindObjectOfType<Text>();
}
    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = rb.velocity.magnitude.ToString("00");
    }
}
