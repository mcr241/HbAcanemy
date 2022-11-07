using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider collider;
    [SerializeField] float speed;
    [SerializeField] float speedAngular;
    [SerializeField] float maxSpace;
    [SerializeField] float timeDelay;
    public IHit owner;


    Vector3 firstPoint;

    public void SetVelocity(Vector3 target)
    {
        firstPoint = transform.position;
        Vector3 vector = target - transform.position;
        vector.y = 0;
        rb.velocity = speed * vector.normalized;
        rb.angularVelocity = new Vector3(0, speedAngular, 0);
    }

    public void SetMaxSpace(float value)
    {
        maxSpace = value;
    }

    private void Update()
    {
        CheckSpace();
    }

    protected void CheckSpace()
    {
        if (Vector3.Distance(firstPoint, transform.position) >= maxSpace)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Cache.GetHit(other) != owner)
        {
            Cache.GetHit(other).GetHit();
            Despawn();
        }
    }

    void Despawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }
}
