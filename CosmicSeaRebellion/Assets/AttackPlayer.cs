using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public int lastAttack;

    public void HandleLightAttack(WeaponObject weapon)
    {
        Player.Instance.animationPlayer.anim.SetInteger("WeaponIndex", weapon.ligthAttackIndex);
        Player.Instance.animationPlayer.PlayTargetAnimationRootMotion("Attack", true);
        lastAttack = weapon.ligthAttackIndex;
    }

    public void HandleWeaponCombo(WeaponObject weapon)
    {

        if (Player.Instance.walkerController.comboFlag)
        {
            Player.Instance.animationPlayer.SetAnimationBool("CanDoCombo", false);

            if (lastAttack == weapon.ligthAttackIndex)
            {
                Player.Instance.animationPlayer.anim.SetInteger("WeaponIndex", weapon.heavyAttackIndex);
                Player.Instance.animationPlayer.PlayTargetAnimationRootMotion("Attack", true);
            }
        }
    }

    public void HandleHeavytAttack(WeaponObject weapon)
    {
        Player.Instance.animationPlayer.anim.SetInteger("WeaponIndex", weapon.heavyAttackIndex);
        Player.Instance.animationPlayer.PlayTargetAnimationRootMotion("Attack", true);
        lastAttack = weapon.heavyAttackIndex;
    }
} 
