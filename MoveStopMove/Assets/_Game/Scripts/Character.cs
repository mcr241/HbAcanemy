using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHit
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject weaponInHand;
    [SerializeField] GameObject weaponThrow;
    [SerializeField] float timeThrow;
    [SerializeField] Range range;
    [SerializeField] float rangeSize;

    protected float timeAttack;
    protected bool isDead = false;
    protected bool isAttacking = false;
    protected string nameAnimTotal = Constant.ANIM_IDLE;
    public void ChangeAnim(string nameAnim)
    {
        if (nameAnim != nameAnimTotal)
        {
            animator.ResetTrigger(nameAnimTotal);
            animator.SetTrigger(nameAnim);
            nameAnimTotal = nameAnim;
        }
    }

    protected void SetRotation(Vector3 distan)
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
        ChangeAnim(Constant.ANIM_DIE);
        isDead = true;
    }

    Character GetCharacterToAttack()
    {
        Character character = range.listCharacter[0];
        for (int i = 1; i < range.listCharacter.Count; i++)
        {
            if (Vector3.Distance(range.listCharacter[i].transform.position, transform.position) < Vector3.Distance(character.transform.position, transform.position))
            {
                character = range.listCharacter[i];
            }
        }
        return character;
    }

    protected bool CanAttack()
    {
        return range.listCharacter != null && range.listCharacter.Count > 0;
    }

    public void Attack()
    {
        SetRotation(GetCharacterToAttack().transform.position - transform.position);
        ChangeAnim(Constant.ANIM_ATTACK);
        timeAttack = 0;
        isAttacking = true;
    }

    protected void AttackInUpdate()
    {
        if (isAttacking)
        {
            timeAttack += Time.deltaTime;
            if (timeAttack >= timeThrow)
            {
                SpawnThrow();
                StopAttack();
            }
        }
    }

    void SpawnThrow()
    {
        Weapon weapon = Instantiate(weaponThrow, weaponInHand.transform.position, weaponInHand.transform.rotation).GetComponent<Weapon>();
        weapon.SetVelocity(GetCharacterToAttack().transform.position);
        weapon.SetMaxSpace(rangeSize);
    }

    public void StopAttack()
    {
        isAttacking = false;
        ChangeAnim(Constant.ANIM_IDLE);
    }
}
