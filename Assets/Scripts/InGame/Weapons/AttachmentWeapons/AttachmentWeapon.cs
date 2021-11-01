using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentWeapon : BaseWeapon, IAttachToCharacter
{
    public void OnAttackPlayer()
    {

    }

    public void OnHit()
    {

    }

    public void OnKilled()
    {

    }

    public void SetActive(bool flag)
    {
        //仮置き
        gameObject.SetActive(false);
    }
}
