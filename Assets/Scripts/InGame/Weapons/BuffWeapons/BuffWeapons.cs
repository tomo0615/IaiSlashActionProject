﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponDefine;

public class BuffWeapons : BaseWeapon
{
    [SerializeField]
    private BuffParams _buffParams;

    public BuffParams GetBuffParams()
    {
        return _buffParams;
    }
}
