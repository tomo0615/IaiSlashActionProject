using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachToCharacter 
{
    void OnAttackPlayer();

    void OnHit();

    void OnKilled();
}
