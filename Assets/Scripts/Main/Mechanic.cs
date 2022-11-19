using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mechanic : MonoBehaviour
{
    public bool isCarEnteredCollision = false;

    // Update is called once per frame (50fps)
    void FixedUpdate()
    {
        EnterCheck();
    }

    public void EnterCheck()
    {
        if (isCarEnteredCollision == true)
        {
            MechanicScene();
        }
    }

    public void MechanicScene()
    {
        SceneManager.LoadScene("Mechanic");
    }
}
