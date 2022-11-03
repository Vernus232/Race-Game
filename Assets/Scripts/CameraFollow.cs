using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Camera cam;
    private float a;
    private float b;

    [Header ("Main options")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    [Header ("FOV")]
    [Range(0, 3)]
    public float fovChangeStrength;


    [Header ("Advanced Options")]
    public int yVelocityCap;
    public int translateSpeedOnCap;
    public int rotationSpeedOnCap;
    [Range(0,1)]
    public float returningValue;

    [Header ("links")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody targetRb;


    public void Start()
    {
        b = rotationSpeed;
        a = translateSpeed;
        cam = FindObjectOfType<Camera>();
    }

    public void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
        FOVChange();
        FollowSpeedChange();
        RotationSpeedChange();
    }

    private void FOVChange()
    {
        cam.fieldOfView = 60 + (Mathf.Abs(targetRb.velocity.x + targetRb.velocity.y + targetRb.velocity.z)*fovChangeStrength);
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
    private void FollowSpeedChange()
    {

        if (targetRb.velocity.y >= yVelocityCap)
        {
            translateSpeed = translateSpeedOnCap;
        }
        if (targetRb.velocity.y < yVelocityCap & translateSpeed < a)
        {
            translateSpeed += returningValue;
        }
    }
    private void RotationSpeedChange()
    {
        if (targetRb.velocity.y >= yVelocityCap)
        {
            rotationSpeed = rotationSpeedOnCap;
        }
        if (targetRb.velocity.y < yVelocityCap & rotationSpeed > b)
        {
            rotationSpeed -= returningValue;
        }
    }
}