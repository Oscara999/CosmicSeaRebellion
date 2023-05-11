using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponHolderSlot leftHandleSlot;
    [SerializeField] WeaponHolderSlot rightHandleSlot;
    [SerializeField] DamageCollider leftDamageCollider;
    [SerializeField] DamageCollider rightDamageCollider;

    void Awake()
    {
        WeaponHolderSlot[] weapondHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weapondSlot in weapondHolderSlots)
        {
            if (weapondSlot.isLeftHandSlot)
            {
                leftHandleSlot = weapondSlot;
            }
            else if (weapondSlot.isRightHandSlot)
            {
                rightHandleSlot = weapondSlot;
            }
        }
    }

    public void LoadWeapondSlot(WeaponObject weapon, bool isLeft)
    {
        if (isLeft)
        {
            leftHandleSlot.LoadWeaponModel(weapon);
            LoadLeftWeaponDamageCollider();
        }
        else
        {
            rightHandleSlot.LoadWeaponModel(weapon);
            LoadRightWeaponDamageCollider();
        }
    }

    #region Handle Weapon's Damage Collider

    void LoadLeftWeaponDamageCollider()
    {
        leftDamageCollider = leftHandleSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    void LoadRightWeaponDamageCollider()
    {
        rightDamageCollider = rightHandleSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    void OpenLeftWeaponDamageCollider()
    {
        leftDamageCollider.ChangeDamageCollider();
    }

    void OpenRightWeaponDamageCollider()
    {
        rightDamageCollider.ChangeDamageCollider();
    }

    #endregion

    public List<GameObject> weaponHandle = new List<GameObject>();
    public void ActivateWeapon(int index)
    {
        for (int i = 0; i < weaponHandle.Count; i++)
        {
            if (index == i)
            {
                weaponHandle[i].SetActive(true);
            }
            else
            {
                weaponHandle[i].SetActive(false);
            }
        }
        
    }
}
