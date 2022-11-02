using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Camera cam;
    [Header ("Основные настройки")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    [Header ("Увеличение FOV")]
    [Range(0, 3)]
    public float fovChangeStrength;
    [Header ("Линки")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody targetRb;


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
}