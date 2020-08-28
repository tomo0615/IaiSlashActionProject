using System.Collections;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Spider
{
    public class SpiderEnemy : BaseEnemy
    {
        [SerializeField] private float explosionTime = 3f;

        [SerializeField] private float attackableRange = 1.5f;
        
        [Inject] private PlayerController playerController;
        
        private void Update() //TODO:EnemyUpdateに変更してManagerクラスのUpdateで呼び出す
        {
            if (currentState == EnemyState.Chase) Chase();
        }

        private void Chase()
        {
            var playerPosition = playerController.transform.position;
            
            var direction = (playerPosition - transform.position).normalized;

            if (Vector2.Distance(playerPosition, transform.position) <= attackableRange)
            {
                StartCoroutine(Explode());
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
            GameEffectManager.Instance.OnGenelateEffect(
                transform.position,
                EffectType.Explosion);

            hitPoint = 0;
        }

        public  override void ApplyDamage()
        {
            hitPoint--;
            hpSubject.OnNext(hitPoint);
        }
    }
}
