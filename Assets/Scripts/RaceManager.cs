using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    [HideInInspector] public int currentCheckpointIndex = 0;
    public int laps;
    private int lapsLeft;
    [HideInInspector] public bool raceStarted = false;
    [HideInInspector] public bool racePassed = false;
    public RaceCheckpoint[] checkpoints;
    public float timeOnStart;
    public float timeLeft;
    public RaceStart start;
    
    [SerializeField] private Collision carCollision;
    [SerializeField] private Image RaceUI;
    [SerializeField] private Text winText;
    [SerializeField] private Text defeatText;
    [SerializeField] private Text checkpointCounter;
    [SerializeField] private Text lapCounter;
    [SerializeField] private Text timerView;


    private void Start() 
    {
        lapsLeft = laps;
        timeLeft = timeOnStart;
    }

    public void UpdateRaceParameters()
    {
        foreach (RaceCheckpoint checkpoint in checkpoints)
        {
            UpdateRaceParameters(checkpoint);
        }
    }

    private void UpdateRaceParameters(RaceCheckpoint checkpoint)
    {
        if (start.raceActivated)
        {
            raceStarted = true;
            OpenUI();
                if (lapsLeft > 1 & start.lapPassed)
                {
                    currentCheckpointIndex = 0;
                    UpdateCheckpoints();
                    checkpoint.passed = false;
                    lapsLeft -= 1;
                    UpdateUI();
                    start.lapPassed = false;
                }
                if (lapsLeft == 1 & start.lapPassed)
                {
                    raceStarted = false;
                    racePassed = true;
                    TurnOffCheckpoints();
                    UpdateUI();
                    start.OnRaceCompleted();
                }
        }
        if (currentCheckpointIndex == 0 & racePassed == false)
        {
            checkpoint.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            UpdateCheckpoints();
        }
    }

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

    private void TurnOffCheckpoints()
    {
        foreach (RaceCheckpoint checkpoint in checkpoints)
        {
            checkpoint.gameObject.SetActive(false);
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

    public void OnCheckpointEnter(float timeAddup)
    {
        timeLeft += timeAddup;

        UpdateRaceParameters();
        UpdateCheckpoints();
        UpdateUI();
        UpdateTimerView();   
    }  


}
