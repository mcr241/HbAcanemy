using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void SetVelocity(Vector3 target)
    {
        base.SetVelocity(target);
        transform.rotation = Quaternion.Euler(0, Vector3.Angle(new Vector3(target.x, 0, target.z), Vector3.forward), 0);
    }
}
