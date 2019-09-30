using IaiAction.Enemys;
using UnityEngine;
using System.Collections;
public class SpiderEnemy : Enemy
{
    [SerializeField]private float explosionTime = 3f;
    private void Update() //TODO:EnemyUpdateに変更してManagerクラスのUpdateで呼び出す
    {
        if (currentState == EnemyState.Chase) Chase();
        else if (currentState == EnemyState.Attack) StartCoroutine(Explode());
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

     private IEnumerator Explode()
    {
        /*数秒間その場にとまってから爆発する
        1.移動のStateを切る DONE
        2.爆破のアニメーションを作動
        3.一定時間後当たり判定を出現（爆破）
        */
       yield return new WaitForSeconds(explosionTime);

        //ココを爆発エフェクトに変更する
       GameEffectManager.Instance.OnGenelateEffect(transform.position,
            GameEffectManager.EffectType.Slash);

        hitPoint = 0;
        Death();
    }

    
}
