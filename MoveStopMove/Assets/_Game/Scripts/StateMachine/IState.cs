using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(Character character);

    public void OnStay(Character character);

    public void OnExit(Character character);
}
