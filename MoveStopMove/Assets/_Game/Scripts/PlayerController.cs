using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Character character;
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
            Vector3 distan = new Vector3(Input.mousePosition.x - fisrtPointJoystick.x, 0, Input.mousePosition.y - fisrtPointJoystick.y);
            float angle = Vector3.Angle(Vector3.forward, distan);
            if (Input.mousePosition.x - fisrtPointJoystick.x < 0) angle = -angle;
            transform.GetChild(0).rotation = Quaternion.Euler(0, angle, 0);

            if (CheckGround())
                Move(distan);
            else
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
                character.ChangeAnim("idle");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isOnJoystick = false;
            character.ChangeAnim("idle");
        }
    }

    bool CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(checkGround.position, Vector2.down, out hit, 1.5f, layerGround))
        {
            /*if (hit.collider.gameObject.GetComponent<BridgeBrick>() != null)
            {
                return hit.collider.gameObject.GetComponent<BridgeBrick>().typeColor == character.typeColor;
            }
            else*/
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void Move(Vector3 distan)
    {
        if (distan.magnitude > 0.5f)
        {
            rigidbody.velocity = new Vector3((distan.normalized * speed).x, rigidbody.velocity.y, (distan.normalized * speed).z);
            character.ChangeAnim("run");
        }
    }
}
