using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constant.ANIM_IDLE);
        character.rb.velocity = new Vector3(0, character.rb.velocity.y, 0);
    }

    public void OnStay(Character character)
    {
        if (character.CanAttack())
        {
            //Debug.Log("attack");
            character.SetState(new AttackState());
        }

    }

    public void OnExit(Character character)
    {

    }
}
