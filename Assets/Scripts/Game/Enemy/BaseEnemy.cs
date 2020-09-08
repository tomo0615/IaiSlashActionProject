using UnityEngine;
using UniRx;
using System;
using Game.Interface;
using Game.Score;
using Zenject;

namespace Game.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float moveSpeed = 10;

        [SerializeField] private int hitPoint = 1;
        
        public ReactiveProperty<int> currentHitPoint;
        
        public EnemyState currentState;

        [Inject] private ScorePresenter scorePresenter;
        
        public virtual void Initialize()
        {
            currentHitPoint = new ReactiveProperty<int>(hitPoint);
            
            currentHitPoint
                .Where(value => value <= 0)
                .Subscribe(_ =>
                { 
                    Debug.Log("Death");
                    Death();
                });
        }
        
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        #region　State用関数
        private void Wait()
        {
            //Debug.Log("Waitなう");
            //待機モーション
        }

        private void Attack()
        {
            //Debug.Log("Attackなう");
            //当たり判定のある何かを出す　or アニメーションを動かす
        }
        private void Chase()
        {
            //Debug.Log("Chaseなう");
            //moveAnimation実装
        }

        public virtual void Freeze()
        {
            //ヒットストップ用（仮）
        }
        #endregion

        public void Death()
        {
            //TODO：死んだときのeffectの追加
            //Debug.Log("死");
            gameObject.SetActive(false);
            //Destroy(gameObject);
            
            scorePresenter.OnChangeScore(10);
        }

        public abstract void ApplyDamage();
    }
}