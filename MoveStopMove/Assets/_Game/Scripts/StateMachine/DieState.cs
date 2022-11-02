using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    float time;
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constant.ANIM_DIE);
        character.isDead = true;
    }

    public void OnStay(Character character)
    {
        time += Time.deltaTime;
        if (time >= character.timeDespawn)
        {
            OnExit(character);
        }
    }

    public void OnExit(Character character)
    {
        character.Despawn();
    }
}
