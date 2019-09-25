using IaiAction.Enemys;
using UnityEngine;

public class SpiderEnemy : Enemy
{
    private void Update()
    {
        if (currentState == EnemyState.Chase) Chase();
        else if (currentState == EnemyState.Attack) Explode();
    }
    private void Chase()
    {
        var playerPos = PlayerManager.Instance.playerTransform.position;

        transform.position += (playerPos - transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    private void Explode()
    {
        /*数秒間その場にとまってから爆発する
        1.移動のStateを切る
        2.爆破のアニメーションを作動
        3.一定時間後当たり判定を出現（爆破）
         */
    }
}
