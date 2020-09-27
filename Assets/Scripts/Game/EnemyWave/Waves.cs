using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.EnemyWave.GUI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.EnemyWave
{
    public class Waves : MonoBehaviour
    {
        [SerializeField]
        private float waveInterval = 1.5f;

        private List<Wave> waveList = new List<Wave>();

        private int currentWaveIndex = 0;
        public Wave CurrentWave => waveList[currentWaveIndex];

        public Wave LastWave => waveList[WaveMaxCount - 1];

        private int WaveMaxCount => waveList.Count;

        private bool HasNextWave => currentWaveIndex + 1 < WaveMaxCount;

        [SerializeField] private WaveTextView waveTextView;

        [SerializeField] private WaveIntervalView waveIntervalView;
        
        private void Awake()
        {
            InitializeWaveList();
        }

        private void Start()
        {
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
            CurrentWave.ActivateWave();
        }

        private IEnumerator OnUpdateNextWave()
        {
            //Waveの変更(ex.1->2)
            CurrentWave.gameObject.SetActive(false);

            currentWaveIndex++;

            waveIntervalView.FillIntervalFrame(waveInterval);
            
            yield return new WaitForSeconds(waveInterval);

            waveIntervalView.ResetFillAmount();
            
            CurrentWave.ActivateWave();
            
            waveTextView.DoMoveAnimation(currentWaveIndex + 1);
        }

        public bool IsEndWave()
        {
            return CurrentWave.IsKillAllEnemy() && currentWaveIndex + 1 == WaveMaxCount;
        }
    }
}
