﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.EnemyWave;
using Game.EnemyWave.GUI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.EnemyGenerator
{
    public class Waves : MonoBehaviour
    {
        [SerializeField]
        private float waveInterval = 1.5f;

        private List<Wave> waveList = new List<Wave>();

        private int currentWaveIndex = 0;
        public Wave CurrentWave => waveList[currentWaveIndex];

        public Wave LastWave => waveList[WaveMaxCount];

        public int WaveMaxCount => waveList.Count;

        public bool HasNextWave => currentWaveIndex + 1 < WaveMaxCount;

        [SerializeField] private WaveTextView waveTextView;
        
        private void Awake()
        {
            InitializeWaveList();
        }

        private void Start()
        {
            OnStartWave(); //GameStateManagerで呼び出す

            //Wave更新用
            this.UpdateAsObservable()
                .Where(_ => CurrentWave.IsKillAllEnemy() && HasNextWave)
                .Subscribe(_ => StartCoroutine(OnUpdateNextWave()));
        }

        private void InitializeWaveList()
        {
            waveList = GetComponentsInChildren<Wave>().Select(
                (wave, index) =>
                {
                    wave.InitializeWave();
                    wave.gameObject.SetActive(false);
                    return wave;
                }).ToList();
        }
        
        //管理系から実行予定
        public void OnStartWave()
        {
            CurrentWave.OnActiveWave();
            
            waveTextView.DoMoveAnimation(currentWaveIndex + 1);
        }

        private IEnumerator OnUpdateNextWave()
        {
            //Waveの変更(ex.1->2)
            CurrentWave.gameObject.SetActive(false);

            currentWaveIndex++;

            yield return new WaitForSeconds(waveInterval);

            CurrentWave.OnActiveWave();
            
            waveTextView.DoMoveAnimation(currentWaveIndex + 1);
        }
    }
}