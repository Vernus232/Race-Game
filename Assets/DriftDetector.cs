using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftDetector : MonoBehaviour
{
    [SerializeField] private GameObject carGameObj;
    private float carRotation;
    private bool driftActive;
    [SerializeField] private ParticleSystem[] driftParticleSystems;
    [SerializeField] private TrailRenderer[] tireLines;

    private void FixedUpdate()
    {
        DriftEnabler();
        DriftAnimation();
    }

    private void DriftEnabler()
    {
        carRotation = carGameObj.transform.localRotation.y;
        print(carRotation);
    }

    private void DriftAnimation()
    {
        if (driftActive == true)
        {
            foreach (ParticleSystem particleSystem in driftParticleSystems)
                particleSystem.Play();
            foreach (TrailRenderer tireLine in tireLines)
                tireLine.gameObject.SetActive(true);
        }
    }
}
