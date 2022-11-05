using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicCollider : MonoBehaviour
{
    [SerializeField] private GameObject car;
    private Mechanic mechanic;
    // Start is called before the first frame update
    void Start()
    {
        mechanic = FindObjectOfType<Mechanic>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(Wait());
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        StopAllCoroutines();
    }

    IEnumerator Wait()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
    
        yield return new WaitForSeconds(2);

        mechanic.isCarEnteredCollision = true;
    }
}
