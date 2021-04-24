using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot lefthandSlot;
    WeaponHolderSlot righthandSlot;

    DamageCollider rightHandGamageCollider;

    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                lefthandSlot = weaponSlot;
            }
            if (weaponSlot.isRightHandSlot)
            {
                righthandSlot = weaponSlot;
            }
        }

    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isRight)
    {
        if(isRight)
        {
            righthandSlot.LoadWeaponModel(weaponItem);
            LoadRightHandWeaponDamageCollider();
        }

        else
        {
            
        }

    }

    private void LoadRightHandWeaponDamageCollider()
    {
        rightHandGamageCollider = righthandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

    }

    public void OpenRightDamageCollider()
    {
        rightHandGamageCollider.EnableDamageCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightHandGamageCollider.DisableDamageCollider();
    }
}
