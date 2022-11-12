using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    [HideInInspector] public bool finishPassed = false;
    [SerializeField] private RaceManager raceManager;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            finishPassed = true;

            raceManager.UpdateUI();
            raceManager.UpdateTimerView();
            raceManager.UpdateCheckpointParameters();
        }
    }

    public void OnRaceCompleted()
    {
        Destroy(gameObject);
    }

    public void OnRaceFailed()
    {
        finishPassed = false;
    }
}
