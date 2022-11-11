using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStart : MonoBehaviour
{
    public float initialTime;
    [HideInInspector] public bool raceActivated = false;
    [HideInInspector] public bool lapPassed = false;
    [SerializeField] private RaceManager raceManager;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Если гонка продолжается
            if (raceActivated)
            {
                raceManager.timeLeft += initialTime;
                raceManager.ResetCheckpoints();
                lapPassed = true;
            }

            // Запуск гонки впервые
            if (raceActivated == false)
            {
                raceActivated = true;
                raceManager.StartTimer();
            }

            raceManager.UpdateUI();
            raceManager.UpdateTimerView();
            raceManager.UpdateRaceParameters();
        }
    }

    public void OnRaceCompleted()
    {
        Destroy(gameObject);
    }

    public void OnRaceFailed()
    {
        lapPassed = false;
        raceActivated = false;
    }





}
