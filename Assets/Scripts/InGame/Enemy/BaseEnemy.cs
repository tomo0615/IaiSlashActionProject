using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using Game.Interface;
using Game.Score;
using Zenject;
using DamageDefine;

namespace Game.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _moveSpeed = 10;

        [SerializeField] private int _hitPoint = 1;
        
        public ReactiveProperty<int> _currentHitPoint;
        
        public EnemyState _currentState;

        private EnemyState _previousState;

        [Inject] private ScorePresenter _scorePresenter = default;
        
        //EnemyStateなどでまとめる
        private bool _isTakeWind = false;

        private bool _isTakeIce = false;

        public virtual void Initialize()
        {
            _currentHitPoint = new ReactiveProperty<int>(_hitPoint);
            
            _currentHitPoint
                .Where(value => value <= 0)
                .Subscribe(_ =>
                {
                    Death();
                });

                _currentState = EnemyState.Wait;
                _previousState = EnemyState.NONE;
        }
        
        public void SetEnemyState(EnemyState state)
        {
            //状態異常がループしてしまうのを防ぐため
            if(IsElementState())
            {
                _previousState = _currentState;
            }

            _currentState = state;

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
        
        private bool IsElementState()
        {
            return true;
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
            gameObject.SetActive(false);

            _scorePresenter.OnChangeScore(10);
        }

        public void TakeDamage(DamageParam damageParam)
        {        
            _currentHitPoint.Value -= damageParam.damageValue;

            TakeElemntDamage(damageParam.damageType);

            if(_isTakeWind)
            {
                TakeAdditionalDamage();
            }
        }

        private void TakeElemntDamage(DamageType damageType)
        {
            //仮パラメータでテスト
            switch(damageType)
            {
                case DamageType.FIRE:
                    StartCoroutine(TakeSlipDamage(1, 5));
                break;

                case DamageType.THUNDER:
                //周囲の敵に感電ダメージ
                break;

                case DamageType.WIND:
                    StartCoroutine(TakeWindCondition());
                break;

                case DamageType.ICE:
                    StartCoroutine(TakeIceCondition());
                break;

                case DamageType.STAN:
                //数秒硬直
                    StartCoroutine(TakeStanCondition(2.0f));
                break;

                default:
                return;
            }
        }
        //EnemyStateでやってみる
        private IEnumerator TakeSlipDamage(int damageValue, int count)
        {
            for(int i = count; i < 0; i--)
            {
                yield return new WaitForSeconds(0.5f);

                _currentHitPoint.Value -= damageValue;
            }
        }

        private IEnumerator TakeWindCondition()
        {
            if(_isTakeWind) yield return null;

            _isTakeWind = true;

            yield return new WaitForSeconds(1.0f);

            _isTakeWind = false;
        }

        //TODO：Flagを参照してUpdateか何かで呼ぶ
        private void TakeAdditionalDamage()
        {
            _currentHitPoint.Value -= 10;
        }

        private IEnumerator TakeIceCondition()
        {
            if(_isTakeIce) yield return null;
            
            SetEnemyState(EnemyState.Freeze);//低速状態Stateに変更する

            yield return new WaitForSeconds(1.0f);

            SetEnemyState(_previousState);
        }

        private IEnumerator TakeStanCondition(float span)
        {
            SetEnemyState(EnemyState.Freeze);

            yield return new WaitForSeconds(1.0f);

            SetEnemyState(_previousState);
        }
    }
}