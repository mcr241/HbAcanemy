using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IState
{
    public void OnEnter(Character character)
    {
        character.ChangeAnim(Constant.ANIM_RUN);
    }

    public void OnStay(Character character)
    {
        
    }

    public void OnExit(Character character)
    {

    }
}
