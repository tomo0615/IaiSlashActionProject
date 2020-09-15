using System.Collections.Generic;
using System.Linq;
using Game.Enemy;
using UnityEngine;

//コンフリクトしないように名前をTestにしてます
namespace Game.EnemyWave
{
    public class Wave : MonoBehaviour
    {
        private  List<BaseEnemy> enemyList = new List<BaseEnemy>();

        public void InitializeWave()
        {
            //子のEnemyをListに格納
            enemyList = GetComponentsInChildren<BaseEnemy>().ToList();
        }

        public void OnActiveWave()
        {
            gameObject.SetActive(true);

            foreach (var enemy in enemyList)
            {
                enemy.Initialize();
            }
        }

        public bool IsKillAllEnemy()
        {
            return GetActiveEnemyValue() == 0;
        }

        private int GetActiveEnemyValue()
        {
            return enemyList.Count(enemy => enemy.gameObject.activeSelf);
        }
    }
}
