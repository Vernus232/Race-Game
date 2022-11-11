using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    [SerializeField] private float timeAddup;
    [HideInInspector] public bool passed = false;
    [SerializeField] private RaceManager raceManager;
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            passed = true;
            raceManager.OnCheckpointEnter(timeAddup);       
        }
    }





}
