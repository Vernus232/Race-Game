using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRace : MonoBehaviour
{
    public float timeForThisCheckpoint;
    [HideInInspector] public bool raceStartActivated = false;
    [HideInInspector] public bool lapPassed = false;
    [SerializeField] private Collider carCollision;
    [SerializeField] private RaceManager raceParameters;

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
        if (raceStartActivated == true)
        {
            raceParameters.ResetCheckpoints();
            lapPassed = true;
            raceParameters.UpdateRaceParameters();
        }
        if (raceStartActivated == false)
        {
            raceStartActivated = true;
            raceParameters.StartTimer();
            raceParameters.UpdateRaceParameters();
        }
        raceParameters.UpdateUI();
        raceParameters.timeLeft += timeForThisCheckpoint;
        raceParameters.UpdateTimerView();
    }
}
