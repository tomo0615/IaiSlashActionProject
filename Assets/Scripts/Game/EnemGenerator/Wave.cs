using IaiAction.Enemys;
using System.Collections.Generic;
using UnityEngine;

//コンフリクトしないように名前をTestにしてます
public class Wave : MonoBehaviour
{
    private  List<BaseEnemy> enemyList = new List<BaseEnemy>();

    public void InitializeWave()
    {
        //子のEnemyをListに格納
        var enemys = GetComponentsInChildren<BaseEnemy>();

        foreach(var enemy in enemys)
        {
            enemyList.Add(enemy);
        }
    }

    public int GetActiveEnemyValue()
    {
        int activeValue = 0;

        foreach (var enemy in enemyList)
        {
            if (enemy.gameObject.activeSelf)
            {
                activeValue++;
            }
        }

        return activeValue;
    }
}
