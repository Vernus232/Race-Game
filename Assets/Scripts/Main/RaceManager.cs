using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public enum RaceType {Sprint,Circuit}
    [HideInInspector] public RaceType raceType;
    [HideInInspector] public bool raceStarted = false;
    [HideInInspector] public bool racePassed = false;
    [HideInInspector] public int currentCheckpointIndex = 0;
    [HideInInspector] public float timeLeft;

    
    public int moneyForRace;
    public int laps;
    public RaceCheckpoint[] checkpoints;
    public float timeOnStart;
    public RaceStart start;
    public RaceFinish finish;
    
    [SerializeField] private Collision carCollision;
    
    [HideInInspector] public int lapsLeft;
    private PlayerUI playerUI;
    private RaceView raceView;

    private void Start() 
    {
        
        lapsLeft = laps;
        timeLeft = timeOnStart;
        playerUI = FindObjectOfType<PlayerUI>();
        raceView = FindObjectOfType<RaceView>(true);
    }

#region Race parameters
    public void UpdateRaceParameters()
    {
        // If start was activated...
        if (start.raceActivated)
        {
            // Starting race and opening UI.
            raceStarted = true;
            raceView.OpenUI();
                // If race have more than one lap, we're reseting every checkpoint after each lap.
                if (lapsLeft > 1 & start.lapPassed)
                {
                    currentCheckpointIndex = 0;
                    UpdateCheckpoints();
                    foreach (RaceCheckpoint checkpoint in checkpoints)
                    {
                        checkpoint.passed = false;
                    }
                    
                    lapsLeft -= 1;
                    raceView.UpdateUI();
                    start.lapPassed = false;
                }
                // If we have less than one lap, race ends when we're crossing start collider.
                if (lapsLeft == 1 & start.lapPassed)
                {
                    EndRace();
                }
                if (raceType == RaceType.Sprint)
                {
                    if (finish.finishPassed)
                    {
                        EndRace();
                    }
                }
        }
        // Everything turns off after new lap starts
        if (currentCheckpointIndex == 0 & racePassed == false)
        {
            ResetCheckpoints();
            start.gameObject.SetActive(false);
            UpdateCheckpoints();
        }
    }

    public IEnumerator RaceTimer()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft -= 1;
            raceView.UpdateTimerView();
        }
        RaceLost();
    }

    public void StartTimer()
    {
        StartCoroutine(RaceTimer());
    }

    private void RaceLost()
    {
        raceView.OnRaceLost();
        StopCoroutine(RaceTimer());
        TurnOffCheckpoints();
        start.gameObject.SetActive(true);
        timeLeft = timeOnStart;
        StartCoroutine(raceView.UICloseTimer());
        start.OnRaceFailed();
    }

    private void EndRace()
    {
        raceStarted = false;
        racePassed = true;
        TurnOffCheckpoints();
        raceView.UpdateUI();
        StopAllCoroutines();
        start.OnRaceCompleted();
        GlobalPlayerData.money += moneyForRace;
        playerUI.UpdatePlayerUI();
        if (raceType == RaceType.Sprint)
        {
            finish.OnRaceCompleted();
        }
    }
#endregion

#region Checkpoints
    public void ResetCheckpoints()
    {
        foreach (RaceCheckpoint checkpoint in checkpoints)
        {
            checkpoint.passed = false;
            currentCheckpointIndex = 0;
            UpdateCheckpoints();
        }
    }
    public void UpdateCheckpoints()
    {
        if (currentCheckpointIndex == 0)
        { 
            checkpoints[currentCheckpointIndex].gameObject.SetActive(true);
        }
        if (checkpoints[currentCheckpointIndex].passed)
        {
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
                checkpoints[currentCheckpointIndex + 1].gameObject.SetActive(true);
                currentCheckpointIndex += 1;
            }
            else
            {
            if (raceType == RaceType.Circuit)
            {
                start.gameObject.SetActive(true);
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
                currentCheckpointIndex += 1;
            }
            if (raceType == RaceType.Sprint)
            {
                finish.gameObject.SetActive(true);
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
            }
            }
            
        }
    }

    private void TurnOffCheckpoints()
    {
        foreach (RaceCheckpoint checkpoint in checkpoints)
        {
            checkpoint.gameObject.SetActive(false);
        }
    }

    public void OnCheckpointEnter(float timeAddup)
    {
        timeLeft += timeAddup;

        UpdateCheckpoints();
        raceView.UpdateUI();
        raceView.UpdateTimerView();   
    } 

#endregion

}
