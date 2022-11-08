using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool checkpointPassed = false;
    [SerializeField] private Collider carCollision;
    [SerializeField] private RaceParameters raceParameters;

    private void Start()
    {
        carCollision = FindObjectOfType<CarController>(CompareTag("Player")).GetComponentInChildren<Collider>();
        raceParameters = FindObjectOfType<RaceParameters>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,3);
    }

    private void OnTriggerEnter(Collider carCollision)
    {
        checkpointPassed = true;
        raceParameters.RaceManager();
        raceParameters.CheckpointManager();
    }
}
