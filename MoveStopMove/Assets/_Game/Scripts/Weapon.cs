using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float maxSpace;

    Vector3 firstPoint;

    private void OnEnable()
    {
        firstPoint = transform.position;
    }
    public void SetVelocity(Vector3 target)
    {
        rb.velocity = speed * (target - transform.position).normalized;
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
        Cache.GetHit(other);
        Despawn();
    }

    void Despawn()
    {
        //Destroy(gameObject);
    }
}
