using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    public bool lapPassed = false;
    [SerializeField] private Collider carCollision;

    private void Start()
    {
        carCollision = FindObjectOfType<CarController>(CompareTag("Player")).GetComponentInChildren<Collider>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position,3);
    }
    private void OnTriggerEnter(Collider carCollision)
    {
        lapPassed = true;
    }
}
