using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    [HideInInspector] public bool finishPassed = false;
    [SerializeField] private RaceManager raceManager;
    private RaceView raceView;

    private void Start() 
    {
        raceView = FindObjectOfType<RaceView>(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            finishPassed = true;

            raceView.UpdateUI();
            raceView.UpdateTimerView();
            raceManager.UpdateRaceParameters();
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
