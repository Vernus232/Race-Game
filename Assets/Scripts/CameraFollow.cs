using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody targetRb;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    

    public void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    public void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
        FOVChange();
    }

    private void FOVChange()
    {
        cam.fieldOfView = 60 + Mathf.Abs(targetRb.velocity.x + targetRb.velocity.y + targetRb.velocity.z);
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}