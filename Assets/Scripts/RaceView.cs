using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaceView : MonoBehaviour
{
    public Image BGImage;
    public Text winText;
    public Text defeatText;
    public Text checkpointCounter;
    public Text lapCounter;
    public Text timerView;
    public RaceManager raceManager;
    public static RaceView main;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    public void OnRaceLost()
    {
        defeatText.gameObject.SetActive(true);
    }
    public void UpdateUI()
    {
        checkpointCounter.text = raceManager.currentCheckpointIndex.ToString("Current Checkpoint : 0");
        if (raceManager.raceType == RaceManager.RaceType.Circuit)
        {
            lapCounter.text = raceManager.lapsLeft.ToString("Laps 0");
        }
        else 
        {
            lapCounter.text = "Sprint!";
        }
        if (raceManager.racePassed == true)
        {
            winText.gameObject.SetActive(true);
            StartCoroutine(UICloseTimer());
            raceManager.timeLeft = 0;
        }
    }

    public void CloseUI()
    {
        defeatText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        checkpointCounter.gameObject.SetActive(false);
        gameObject.SetActive(false);
        timerView.gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        checkpointCounter.gameObject.SetActive(true);
        gameObject.SetActive(true);
        lapCounter.gameObject.SetActive(true);
        timerView.gameObject.SetActive(true);
    }

    public IEnumerator UICloseTimer()
    {
        yield return new WaitForSeconds(5);
        CloseUI();
    }

    public void UpdateTimerView()
    {
        timerView.text = raceManager.timeLeft.ToString("Time : 0.00"); 
    }
}
