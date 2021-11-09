using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponDefine;
using Zenject;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]private WeaponSpawner _weaponSpawner;

    [SerializeField]private List<Transform> _spawnTransformList;

    [SerializeField]private BaseWeapon _testSpawnWeapon;//ここを武器をまとめたものに変更

    private WeaponTable _weaponTable = default;

    [Inject]
    private void Construct(WeaponTable weaponTable)
    {
        _weaponTable = weaponTable;

        _weaponSpawner.Init();
    }

    //TODO:ゲーム進行するManagerで呼び出す
    public void StartWeaponSelect()
    {
        ClearSpawnedWeapon();

        //TODO:IDで被りかどうか調べる

        for(int i = 0; i < 3; i++)
        {
            _weaponSpawner.SpawnWeapon(_spawnTransformList[i], GetRandomWeapon());
        }
    }

    public void OnEndWeponSelect()
    {
        ClearSpawnedWeapon();
    }

    private BaseWeapon GetRandomWeapon()
    {
        //現在の進行度にあわせてだす
        //ScriptableObjectで管理する？
        return _testSpawnWeapon;
    }

    public BaseWeapon GetSpawnedWeapon(int positionIndex)
    {
        return _weaponSpawner.GetSpawnedWeapon(positionIndex);
    }

    public BaseWeapon GetWeaponNearestPlayer(Vector3 playerPosition)
    {
        int index = 0;
        float distance = 50.0f;//仮置き

        for(int i = 0; i < 3; i++)
        {
            if(Vector3.Distance(playerPosition, _spawnTransformList[i].position) <= distance)
            {
                return _weaponSpawner.GetSpawnedWeapon(i);
            }
        }

        return null;
    }

    private void ClearSpawnedWeapon()
    {
        _weaponSpawner.ClearWeapon();
    }
}
