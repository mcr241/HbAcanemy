using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] float timeDelayMove;
    [SerializeField] NavMeshAgent nav;
    [SerializeField] float rangeMove;

    float time;

    private void OnEnable()
    {
        SetState(new MoveState());
    }
    private void Update()
    {
        currentstate.OnStay(this);
        time += Time.deltaTime;
        if (!isDead)
        {
            if (time > timeDelayMove && currentstate is IdleState && !CanAttack())
            {
                SetState(new MoveState());
                time = 0;
            }
        }
    }

    public override void Despawn()
    {
        LevelManager.Instance.SpawnBot();
        //transform.position = 100 * Vector3.one;
        base.Despawn();
    }

}
