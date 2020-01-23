using System.Collections.Generic;
using UnityEngine;

//コンフリクトしないように名前をTestにしてます
public class WaveTest : MonoBehaviour
{
    public List<EnemyTest> enemyList
    {
        get; private set;
    }
    = new List<EnemyTest>();

    public void InitializeWave()
    {
        //子のEnemyをListに格納
        var enemys = GetComponentsInChildren<EnemyTest>();

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
