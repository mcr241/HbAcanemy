using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time;
    bool checkSpawn = false;
    Vector3 target;
    public void OnEnter(Character character)
    {
        GameObject character1 = character.GetCharacterToAttack();
        if (character1 != null)
        {
            target = character1.transform.position;
            character.SetRotation(target - character.transform.position);
            character.ChangeAnim(Constant.ANIM_ATTACK);
            time = 0;
            checkSpawn = false;
        }
        else
        {
            Debug.Log("idle");
            character.SetState(new IdleState());
        }
    }

    public void OnStay(Character character)
    {
        time += Time.deltaTime;
        if (!checkSpawn)
        {
            if (time >= character.timeThrow)
            {
                checkSpawn = true;
                character.SpawnThrow(target);
            }
        }
        else
        {
            if (time >= character.timeThrow * 2)
            {
                character.SetState(new IdleState());
            }
        }
    }

    public void OnExit(Character character)
    {

    }
}
