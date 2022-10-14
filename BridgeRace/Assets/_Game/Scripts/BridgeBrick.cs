using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBrick : MonoBehaviour
{
    public TypeColor typeColor;

    Character character;

    private void OnTriggerEnter(Collider other)
    {
        character = other.GetComponent<Character>();
        if (character.typeColor != typeColor && character.CanSubBrick())
        {
            character.SubBrick();
            typeColor = character.typeColor;
            transform.GetComponentInChildren<MeshRenderer>().material = character.material;
        }
        else
        {
            character.isOnBridge = false;
        }
    }
}
