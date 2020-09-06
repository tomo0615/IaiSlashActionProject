using UnityEngine;
using UniRx;
using System;
using Game.Interface;
using Game.Score;
using Zenject;

public enum EnemyState
{
    Wait,
    Chase,
    Attack,
    Freeze
};

namespace Game.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        #region パラメータ
        [SerializeField] public int hitPoint = 1;

        [SerializeField] public float moveSpeed = 10;
        #endregion

        public Subject<int> hpSubject = new Subject<int>();

        public IObservable<int> OnHpChanged => hpSubject;

        public EnemyState currentState;

        [Inject] private ScorePresenter scorePresenter;
        
        private void Start()
        {
            this.OnHpChanged
                .Subscribe(_ =>
                {
                    if (hitPoint <= 0) Death();
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