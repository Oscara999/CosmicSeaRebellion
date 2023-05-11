using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "ScriptableObjects/Items/Weapon", order = 3)]
public class WeaponObject : ItemObject
{
    public GameObject model;
    public bool isUnarmed;
    public TypeWeapon TypeWeapon;

    [Header("IdleAnimation")]
    public string rigthHandIdle;
    public string leftHandIdle;


    [Header("AttackAnimation")]
    public int ligthAttackIndex;
    public int heavyAttackIndex;


}
