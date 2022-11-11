using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    [HideInInspector] public bool raceStarted = false;
    [HideInInspector] public bool racePassed = false;
    [HideInInspector] public int currentCheckpointIndex = 0;
    [HideInInspector] public float timeLeft;

    public int laps;
    public RaceCheckpoint[] checkpoints;
    public float timeOnStart;
    public RaceStart start;
    
    [SerializeField] private Collision carCollision;
    [SerializeField] private Image RaceUI;
    [SerializeField] private Text winText;
    [SerializeField] private Text defeatText;
    [SerializeField] private Text checkpointCounter;
    [SerializeField] private Text lapCounter;
    [SerializeField] private Text timerView;

    private int lapsLeft;


    private void Start() 
    {
        lapsLeft = laps;
        timeLeft = timeOnStart;
    }

#region Race parameters
    private void UpdateRaceParameters(RaceCheckpoint checkpoint)
    {
        // If start was activated...
        if (start.raceActivated)
        {
            // Starting race and opening UI.
            raceStarted = true;
            OpenUI();
                // If race have more than one lap, we're reseting every checkpoint after each lap.
                if (lapsLeft > 1 & start.lapPassed)
                {
                    currentCheckpointIndex = 0;
                    UpdateCheckpoints();
                    checkpoint.passed = false;
                    lapsLeft -= 1;
                    UpdateUI();
                    start.lapPassed = false;
                }
                // If we have less than one lap, race ends when we're crossing start collider.
                if (lapsLeft == 1 & start.lapPassed)
                {
                    raceStarted = false;
                    racePassed = true;
                    TurnOffCheckpoints();
                    UpdateUI();
                    start.OnRaceCompleted();
                }
        }
        // Everything turns off after new lap starts
        if (currentCheckpointIndex == 0 & racePassed == false)
        {
            checkpoint.gameObject.SetActive(false);
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
            UpdateTimerView();
        }
        RaceLost();
    }

    public void StartTimer()
    {
        StartCoroutine(RaceTimer());
    }

    private void RaceLost()
    {
        defeatText.gameObject.SetActive(true);
        StopCoroutine(RaceTimer());
        TurnOffCheckpoints();
        start.gameObject.SetActive(true);
        timeLeft = timeOnStart;
        StartCoroutine(UICloseTimer());
        start.OnRaceFailed();
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
            UpdateRaceParameters(checkpoint);
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
                start.gameObject.SetActive(true);
                checkpoints[currentCheckpointIndex].gameObject.SetActive(false);
                currentCheckpointIndex += 1;
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

        UpdateCheckpointParameters();
        UpdateCheckpoints();
        UpdateUI();
        UpdateTimerView();   
    } 

    public void UpdateCheckpointParameters()
    {
        foreach (RaceCheckpoint checkpoint in checkpoints)
        {
            UpdateRaceParameters(checkpoint);
        }
    }
#endregion

#region User Interface
    public void UpdateUI()
    {
        checkpointCounter.text = currentCheckpointIndex.ToString("Current Checkpoint : 0");
        lapCounter.text = lapsLeft.ToString("Laps 0");

        if (racePassed == true)
        {
            winText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(UICloseTimer());
            timeLeft = 0;
        }
    }
    
    public void UpdateTimerView()
    {
        timerView.text = timeLeft.ToString("Time : 0.00"); 
    }

    private void CloseUI()
    {
        defeatText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        checkpointCounter.gameObject.SetActive(false);
        RaceUI.gameObject.SetActive(false);
        timerView.gameObject.SetActive(false);
    }

    private void OpenUI()
    {
        checkpointCounter.gameObject.SetActive(true);
        RaceUI.gameObject.SetActive(true);
        lapCounter.gameObject.SetActive(true);
        timerView.gameObject.SetActive(true);
    }

    private IEnumerator UICloseTimer()
    {
        yield return new WaitForSeconds(5);
        CloseUI();
    }
#endregion

}
