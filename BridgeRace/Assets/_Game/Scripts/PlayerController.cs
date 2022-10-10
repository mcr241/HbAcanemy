using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    Vector3 fisrtPointJoystick;
    bool isOnJoystick;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fisrtPointJoystick = Input.mousePosition;
            isOnJoystick = true;
        }
        
        if (isOnJoystick)
        {
            if (CheckGround())
                Move();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isOnJoystick = false;
        }
    }

    bool CheckGround()
    {
        return Physics.Raycast(checkGround.position, Vector2.down, 1.1f, layerGround);
    }

    private void Move()
    {
        Vector3 distan = new Vector3(Input.mousePosition.x - fisrtPointJoystick.x, 0, Input.mousePosition.y - fisrtPointJoystick.y);
        if (distan.magnitude > 0.5f)
        {
            rigidbody.velocity = new Vector3((distan.normalized * speed).x, rigidbody.velocity.y, (distan.normalized * speed).z);
            float angle = Vector2.Angle(Vector2.up, Input.mousePosition);
         
            transform.GetChild(0).rotation = Quaternion.Euler(0, angle, 0);
        }
    }

}
