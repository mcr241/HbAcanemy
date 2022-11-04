using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider collider;
    [SerializeField] float speed;
    [SerializeField] float speedAngular;
    [SerializeField] float maxSpace;
    [SerializeField] float timeDelay;

    float time = 0;

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
        collider.enabled = false;
        time = 0;
    }

    public void SetMaxSpace(float value)
    {
        maxSpace = value;
    }

    private void Update()
    {
        CheckSpace();
        if (!collider.enabled)
        {
            time += Time.deltaTime;
            if (time > timeDelay)
            {
                collider.enabled = true;
            }
        }
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
