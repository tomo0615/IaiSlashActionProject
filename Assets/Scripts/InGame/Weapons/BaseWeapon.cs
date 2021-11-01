using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponDefine;
public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaoponData _weaponData;

    public WeaponType GetWeaponType()
    {
        return _weaponData.weaponType;
    }
}
