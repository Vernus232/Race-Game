using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Camera cam;
    private float translateSpeedDefault;
    private float rotationSpeedDefault;

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
        rotationSpeedDefault = rotationSpeed;
        translateSpeedDefault = translateSpeed;
        cam = FindObjectOfType<Camera>();
        target = FindObjectOfType<CarController>().transform;
        targetRb = FindObjectOfType<Rigidbody>(CompareTag("Player"));
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
        cam.fieldOfView = 50 + targetRb.velocity.magnitude * fovChangeStrength;
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
        if (targetRb.velocity.y < yVelocityCap & translateSpeed < translateSpeedDefault)
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
        if (targetRb.velocity.y < yVelocityCap & rotationSpeed > rotationSpeedDefault)
        {
            rotationSpeed -= returningValue;
        }
    }
}