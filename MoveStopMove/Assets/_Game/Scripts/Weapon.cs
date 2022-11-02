using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float speedAngular;
    [SerializeField] float maxSpace;

    Vector3 firstPoint;

    private void OnEnable()
    {
        firstPoint = transform.position;
    }
    public void SetVelocity(Vector3 target)
    {
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
        Cache.GetHit(other).GetHit();
        Despawn();
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}
