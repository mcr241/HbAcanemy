using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] GameObject chestClose;
    [SerializeField] GameObject chestOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            effect.SetActive(true);
            chestClose.SetActive(false);
            chestOpen.SetActive(true);
            other.GetComponent<Player>().Win();
        }
    }
}
