using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponController weaponController;

    public WeaponObject rightWeapon;
    public WeaponObject leftWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        weaponController = GetComponentInChildren<WeaponController>();
    }

    void Start()
    {
        weaponController.LoadWeapondSlot(rightWeapon, false);
        weaponController.LoadWeapondSlot(leftWeapon, true);
    }
}
