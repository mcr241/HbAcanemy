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
        character.rb.velocity = new Vector3(0, character.rb.velocity.y, 0);
    }

    public void OnStay(Character character)
    {
        time += Time.deltaTime;
        if (time >= character.timeDespawn)
        {
            character.Despawn();
        }
    }

    public void OnExit(Character character)
    {
    }
}
