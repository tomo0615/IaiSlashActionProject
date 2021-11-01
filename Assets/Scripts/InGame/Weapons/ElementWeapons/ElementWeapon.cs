using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponDefine;

public class ElementWeapon : BaseWeapon
{
    [SerializeField]
    private ElementType _elementType;

    public ElementType GetElementType()
    {
        return _elementType;
    }
}
