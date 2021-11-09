using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private List<BaseWeapon> _spawnedWeaponList;

    public void Init()
    {
        _spawnedWeaponList = new List<BaseWeapon>();
    }

    public void SpawnWeapon(Transform transfrom, BaseWeapon baseWeapon)
    {
       Instantiate(baseWeapon, transfrom);
    }

    public BaseWeapon GetSpawnedWeapon(int positionIndex)
    {
       return _spawnedWeaponList[positionIndex];
    }

    public void ClearWeapon()
    {
        for(int i = 0; i < 3; i++)
        {
            Destroy(_spawnedWeaponList[i]);
        }

        _spawnedWeaponList.Clear();
    }

}
