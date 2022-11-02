using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, IHit> keyValuePairs = new Dictionary<Collider, IHit>(); 

    public static IHit GetHit(Collider collider)
    {
        if (!keyValuePairs.ContainsKey(collider))
        {
            keyValuePairs.Add(collider, collider.GetComponent<IHit>());
        }
        return keyValuePairs[collider];
    }
}
