using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IState
{
    public void OnEnter(Character character)
    {

    }

    public void OnStay(Character character)
    {
        
    }

    public void OnExit(Character character)
    {
        character.rb.velocity = new Vector3(0, character.rb.velocity.y, 0);
        character.ChangeAnim(Constant.ANIM_IDLE);
        character.SetState(new IdleState());
    }
}
