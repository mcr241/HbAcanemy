using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang : Weapon
{

    [SerializeField] float speedAngular;
    [SerializeField] float speedBack;
    public override void OnDespawn()
    {
        rb.velocity = speedBack * ((owner as Character).transform.position - transform.position).normalized;
        rb.angularVelocity = new Vector3(0, speedAngular, 0);
    }
}
