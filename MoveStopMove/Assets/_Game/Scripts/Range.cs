using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public List<Character> listCharacter;
    [SerializeField] SphereCollider sphere;
    [SerializeField] LayerMask layerMask;
    

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

    public Collider GetCharacterNear()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, sphere.radius, Vector3.zero, out hit, Mathf.Infinity, layerMask);
        return hit.collider;
    }

    public void SetRange(float range)
    {
        sphere.radius = range;
    }
}
