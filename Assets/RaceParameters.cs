using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceParameters : MonoBehaviour
{
    public int laps;
    public bool RacePassed = false;
    public Checkpoint[] checkpoints;
    public StartRace start;
    [SerializeField] private Collision carCollision;

    void Update()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (laps > 1 & start.lapPassed)
            {
                checkpoint.checkpointPassed = false;
                start.lapPassed = false;
                laps -= 1;
            }
            if (laps == 1 & start.lapPassed)
            {
                RacePassed = true;
            }
        }
    }
}
