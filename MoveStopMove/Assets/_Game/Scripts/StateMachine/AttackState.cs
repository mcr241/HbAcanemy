using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time;
    bool checkSpawn = false;
    public void OnEnter(Character character)
    {
        character.SetRotation(character.GetCharacterToAttack().transform.position - character.transform.position);
        character.ChangeAnim(Constant.ANIM_ATTACK);
        time = 0;
        checkSpawn = false;
    }

    public void OnStay(Character character)
    {
        if (!checkSpawn)
        {
            time += Time.deltaTime;
            if (time >= character.timeThrow)
            {
                checkSpawn = true;
                character.SpawnThrow();
                character.ChangeAnim(Constant.ANIM_IDLE);
                //OnExit(character);
            }
        }
    }

    public void OnExit(Character character)
    {

    }
}
