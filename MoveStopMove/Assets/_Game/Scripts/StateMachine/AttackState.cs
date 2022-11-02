using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time;
    public void OnEnter(Character character)
    {
        character.SetRotation(character.GetCharacterToAttack().transform.position - character.transform.position);
        character.ChangeAnim(Constant.ANIM_ATTACK);
        time = 0;
    }

    public void OnStay(Character character)
    {
        time += Time.deltaTime;
        if (time >= character.timeThrow)
        {
            character.SpawnThrow();
            OnExit(character);
        }
    }

    public void OnExit(Character character)
    {
        character.ChangeAnim(Constant.ANIM_IDLE);
    }
}
