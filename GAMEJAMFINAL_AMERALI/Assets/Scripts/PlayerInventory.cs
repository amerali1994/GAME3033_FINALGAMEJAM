using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;
    public WeaponItem rightHandweapon;
    public WeaponItem leftHandweapon;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {

        weaponSlotManager.LoadWeaponOnSlot(rightHandweapon, false);
  
        weaponSlotManager.LoadWeaponOnSlot(leftHandweapon, true);

    }
}
