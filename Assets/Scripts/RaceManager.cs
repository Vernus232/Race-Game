using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public int currentCheckpointIndex = 0;
    public int laps;
    private int lapsLeft;
    public bool raceStarted = false;
    public bool racePassed = false;
    public Checkpoint[] checkpoints;
    public StartRace start;
    [SerializeField] private Collision carCollision;
    [SerializeField] private Image RaceUI;
    [SerializeField] private Text winText;
    [SerializeField] private Text checkpointCounter;

    private void Start() 
    {
        lapsLeft = laps;
    }

    public void UpdateRaceParameters()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            SmallMethod1(checkpoint);
        }
    }

    private void SmallMethod1(Checkpoint checkpoint)
    {
        if (start.raceStartActivated)
        {
            raceStarted = true;
            checkpointCounter.gameObject.SetActive(true);
            RaceUI.gameObject.SetActive(true);
            if (checkpoint.checkpointPassed)
            {
                if (lapsLeft > 1 & start.lapPassed)
                {
                    currentCheckpointIndex = 0;
                    UpdateCheckpoints();
                    checkpoint.checkpointPassed = false;
                    lapsLeft -= 1;
                    start.lapPassed = false;
                }
                if (lapsLeft == 1 & start.lapPassed)
                {
                    raceStarted = false;
                    racePassed = true;
                    winText.gameObject.SetActive(true);
                }
            }
        }
        if (currentCheckpointIndex == 0)
        {
            checkpoint.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            UpdateCheckpoints();
        }
    }

    public void ResetCheckpoints()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.checkpointPassed = false;
        }
    }

    public void UpdateCheckpoints()
    {
        if (currentCheckpointIndex == 0)
        { 
            checkpoints[currentCheckpointIndex].gameObject.SetActive(true);
        }
        if (checkpoints[currentCheckpointIndex].checkpointPassed)
        {
            if (currentCheckpointIndex < checkpoints.Length - 1)
            {
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
                checkpoints[currentCheckpointIndex + 1].gameObject.SetActive(true);
                currentCheckpointIndex += 1;
            }
            else
            {
                start.gameObject.SetActive(true);
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
            }
            checkpointCounter.text = (currentCheckpointIndex.ToString("Current Checkpoint : 0"));
        }
    }
}
