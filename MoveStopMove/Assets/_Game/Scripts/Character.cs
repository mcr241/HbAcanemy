using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit, IHit
{
    public Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] GameObject weaponInHand;
    [SerializeField] GameUnit weaponThrow;
    public float timeThrow;
    public float timeDespawn;
    [SerializeField] Range range;
    [SerializeField] float rangeSize;
    public float speed;

    protected float timeAttack;
    protected float timeDie;
    public bool isDead = false;
    protected bool isAttacking = false;
    protected string nameAnimTotal = Constant.ANIM_IDLE;
    public IState currentstate = new IdleState();
    public void ChangeAnim(string nameAnim)
    {
        if (nameAnim != nameAnimTotal)
        {
            animator.ResetTrigger(nameAnimTotal);
            animator.SetTrigger(nameAnim);
            nameAnimTotal = nameAnim;
        }
    }

    public void SetRotation(Vector3 distan)
    {
        float angle = Vector3.Angle(Vector3.forward, distan);
        if (distan.x < 0) angle = -angle;
        transform.GetChild(0).rotation = Quaternion.Euler(0, angle, 0);
    }

    public void GetHit()
    {
        if (!isDead)
            Die();
    }

    void Die()
    {
        SetState(new DieState());
    }
    public GameObject GetCharacterToAttack()
    {
        /*if (!CanAttack())
            return null;
        int j=0;
        while (j < range.listCharacter.Count && range.listCharacter[j].isDead)
        {
            j++;
        }
        if (j >= range.listCharacter.Count) return null;

        Character character = range.listCharacter[j];
        for (int i = j + 1; i < range.listCharacter.Count; i++)
        {
            if (!range.listCharacter[i].isDead && Vector3.Distance(range.listCharacter[i].transform.position, transform.position) < Vector3.Distance(character.transform.position, transform.position))
            {
                character = range.listCharacter[i];
            }
        }*/
        return range.GetCharacterNear().gameObject;
    }

    public bool CanAttack()
    {
        return range.GetCharacterNear() != null;
    }

    public void SpawnThrow(Vector3 target)
    {
        Weapon weapon = SimplePool.Spawn(weaponThrow, weaponInHand.transform.position, Quaternion.identity).GetComponent<Weapon>();
        weapon.SetVelocity(target);
        weapon.SetMaxSpace(rangeSize);
        weapon.owner = this;
    }

    public void SetState(IState state)
    {
        if (currentstate != state)
        {
            currentstate.OnExit(this);

            currentstate = state;
            currentstate.OnEnter(this);
        }
    }

    public virtual void Despawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        currentstate = new IdleState();
        rb.velocity = Vector3.zero;
        isDead = false;
    }

    public override void OnDespawn()
    {
    }
}
