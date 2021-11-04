using System.Collections;
using Game.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Enemy.Spider
{
    public class SpiderEnemy : BaseEnemy
    {
        [SerializeField] private float explosionTime = 3f;

        [SerializeField] private float attackableRange = 1.5f;
        
        [Inject] private PlayerController playerController = default;

        public override void Initialize()
        {
            base.Initialize();
            
            this.UpdateAsObservable()
                .Where(_ => _currentState == EnemyState.Chase)
                .Subscribe(_ =>
                {
                    Chase();
                });
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

            playerPosition.y = transform.position.y; //上方向にむかせないため
            transform.LookAt(playerPosition);
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }

        private IEnumerator Explode()
        {
            /*
            数秒間その場にとまってから爆発する
            1.移動のStateを切る DONE
            2.爆破のアニメーションを作動
            3.一定時間後当たり判定を出現（爆破）
            */
            yield return new WaitForSeconds(explosionTime);

            GameEffectManager.Instance.OnGenelateEffect(
                transform.position,
                EffectType.Explosion);

            _currentHitPoint.Value = 0;
        }
    }
}
