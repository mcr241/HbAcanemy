using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnEnter(Character character)
    {

    }

    public void OnStay(Character character)
    {
        if (character.CanAttack()) character.SetState(new AttackState());
    }

    public void OnExit(Character character)
    {

    }
}
