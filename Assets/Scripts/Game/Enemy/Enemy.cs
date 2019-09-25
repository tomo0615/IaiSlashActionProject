using UnityEngine;

namespace IaiAction.Enemys
{
    public abstract class Enemy : MonoBehaviour
    {
        #region パラメータ
        [SerializeField] public int hitPoint = 1;

        [SerializeField] public float moveSpeed = 10;
        #endregion

        public enum EnemyState
        {
            Wait,
            Chase,
            Attack,
            Freeze
        };

        public EnemyState currentState;

        public void SetEnemyState(EnemyState state)
        {
            currentState = state;

            switch (state)
            {
                case EnemyState.Wait:
                    Wait();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
                case EnemyState.Freeze:
                    Freeze();
                    break;
            }
        }

        #region　State用関数
        private void Wait()
        {
            Debug.Log("Waitなう");
            //待機モーション
        }

        private void Attack()
        {
            Debug.Log("Attackなう");
            //当たり判定のある何かを出す　or アニメーションを動かす
        }
        private void Chase()
        {
            Debug.Log("Chaseなう");
            //moveAnimation実装
        }

        public virtual void Freeze()
        {
            //ヒットストップ用（仮）
        }
        #endregion

        private void Death()
        {
            if (hitPoint > 0) return;

            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Attack"))
            {
                hitPoint--;

                GameEffectManager.Instance.ShakeCamera(0.25f);
                GameEffectManager.Instance.OnHitAttack(other.transform.position);
                Death();
            }
        }
    }
}