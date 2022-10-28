using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    Vector3 fisrtPointJoystick;
    bool isOnJoystick;
    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fisrtPointJoystick = Input.mousePosition;
                isOnJoystick = true;
            }

            if (isOnJoystick)
            {
                Vector3 distan = new Vector3(Input.mousePosition.x - fisrtPointJoystick.x, 0, Input.mousePosition.y - fisrtPointJoystick.y);
                SetRotation(distan);

                if (CheckGround())
                    Move(distan);
                else
                {
                    StopMove();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isOnJoystick = false;
                StopMove();
            }

            AttackInUpdate();
        }
    }


    bool CheckGround()
    {
        RaycastHit hit;
        return Physics.Raycast(checkGround.position, Vector2.down, out hit, 1.5f, layerGround);
    }

    private void Move(Vector3 distan)
    {
        if (distan.magnitude > 0.5f)
        {
            StopAttack();
            rb.velocity = new Vector3((distan.normalized * speed).x, rb.velocity.y, (distan.normalized * speed).z);
            ChangeAnim(Constant.ANIM_RUN);
        }
        else
        {
            StopMove();
        }
    }

    void StopMove()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        ChangeAnim(Constant.ANIM_IDLE);
        if (CanAttack()) Attack();
    }
}
