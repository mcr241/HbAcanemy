using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public List<Character> listCharacter;
    [SerializeField] SphereCollider sphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            listCharacter.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            listCharacter.Remove(other.GetComponent<Character>());
        }
    }

    public void SetRange(float range)
    {
        sphere.radius = range;
    }
}
