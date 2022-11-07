using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechanicCollider : MonoBehaviour
{
    public Text text;
    private GameObject car;
    private Mechanic mechanic;
    // Start is called before the first frame update
    void Start()
    {
        mechanic = FindObjectOfType<Mechanic>();
        car = FindObjectOfType<GameObject>(CompareTag("Player"));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            StartCoroutine(Wait());
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        StopAllCoroutines();
        text.gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

        mechanic.isCarEnteredCollision = true;
    }
}
