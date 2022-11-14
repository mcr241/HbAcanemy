using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] float speedAngular;
    public override void SetVelocity(Vector3 target)
    {
        base.SetVelocity(target);
        rb.angularVelocity = new Vector3(0, speedAngular, 0);
    }
}
