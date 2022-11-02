using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] float speed;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    Vector3 fisrtPointJoystick;
    bool isOnJoystick;


    bool CheckGround()
    {
        RaycastHit hit;
        return Physics.Raycast(checkGround.position, Vector2.down, out hit, 1.5f, layerGround);
    }


    private void Update()
    {
        currentstate.OnStay(this);

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
                {
                    if (Vector3.Distance(Input.mousePosition, fisrtPointJoystick) > 0.5f)
                    {
                        SetState(new PlayerController());
                        rb.velocity = new Vector3((distan.normalized * speed).x, rb.velocity.y, (distan.normalized * speed).z);
                        ChangeAnim(Constant.ANIM_RUN);
                    }
                    else
                    {
                        SetState(new IdleState());
                    }
                }
                else
                {
                    SetState(new IdleState());
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isOnJoystick = false;
                SetState(new IdleState());
            }
        }
    }

}
