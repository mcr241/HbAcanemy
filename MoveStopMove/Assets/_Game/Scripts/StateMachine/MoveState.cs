using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    Vector3 endPoint;

    public void OnEnter(Character character)
    {
        endPoint = NavMeshUtil.GetRandomPoint(character.transform.position, 20);
        character.SetRotation(endPoint- character.transform.position);
        character.ChangeAnim(Constant.ANIM_RUN);
    }

    public void OnStay(Character character)
    {
        character.rb.velocity = (endPoint - character.transform.position).normalized * character.speed;

        if (Vector3.Distance(character.transform.position, endPoint) < 0.1f)
        {
            character.SetState(new IdleState());
        }
    }

    public void OnExit(Character character)
    {
        //character.rb.velocity = new Vector3(0, character.rb.velocity.y, 0);
    }
}
