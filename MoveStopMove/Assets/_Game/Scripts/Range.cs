using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] SphereCollider sphere;
    [SerializeField] LayerMask layerMask;
    float radius = 5;
    

    public Collider GetCharacterNear()
    {
        Collider[] hitColliders = new Collider[LevelManager.Instance.botsInMap.Count];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, hitColliders, layerMask);
        if (numColliders > 0)
        {
            float minDis = Mathf.Infinity;
            int num = -1;
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].gameObject != transform.parent.gameObject && hitColliders[i].gameObject.GetComponent<Character>() != null &&  minDis > Vector3.Distance(transform.position, hitColliders[i].transform.position))
                {
                    num = i;
                    minDis = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                }
            }
            if (num > 0)
                return hitColliders[num];
            else return null;
        }
        else return null;
    }

    public void SetRange(float range)
    {
        radius = range;
    }
}
