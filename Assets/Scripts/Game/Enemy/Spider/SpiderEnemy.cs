using IaiAction.Enemys;
using UnityEngine;

public class SpiderEnemy : Enemy
{
    private void Update() //TODO:EnemyUpdateに変更してManagerクラスのUpdateで呼び出す
    {
        if (currentState == EnemyState.Chase) Chase();
        else if (currentState == EnemyState.Attack) Explode();
    }

    private void Chase()
    {
        var playerPos = PlayerManager.Instance.playerTransform.position;

        var direction = (playerPos - transform.position).normalized;

        if (Vector2.Distance(playerPos, transform.position) <= 1.5f)
        {
            SetEnemyState(EnemyState.Attack);
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Explode()
    {
        /*数秒間その場にとまってから爆発する
        1.移動のStateを切る DONE
        2.爆破のアニメーションを作動
        3.一定時間後当たり判定を出現（爆破）
        */

       
    }

    
}
