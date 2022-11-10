using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float timeForThisCheckpoint;
    [HideInInspector] public bool checkpointPassed = false;
    [SerializeField] private Collider carCollision;
    [SerializeField] private RaceManager raceParameters;

    private void Start()
    {
        carCollision = FindObjectOfType<CarController>(CompareTag("Player")).GetComponentInChildren<Collider>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,3);
    }

    private void OnTriggerEnter(Collider carCollision)
    {
        checkpointPassed = true;
        raceParameters.timeLeft += timeForThisCheckpoint;
        raceParameters.UpdateRaceParameters();
        raceParameters.UpdateCheckpoints();
        raceParameters.UpdateUI();
    }
}
