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

        private List<Wave> _waveList = new List<Wave>();

        private int _currentWaveIndex = 0;
        
        private Wave CurrentWave => _waveList[_currentWaveIndex];
        
        private int WaveMaxCount => _waveList.Count;

        private bool HasNextWave => _currentWaveIndex + 1 < WaveMaxCount;

        [SerializeField] private WaveTextView waveTextView = default;

        [SerializeField] private WaveIntervalView waveIntervalView = default;
        
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
            _waveList = GetComponentsInChildren<Wave>().Select(
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

            _currentWaveIndex++;

            waveIntervalView.FillIntervalFrame(waveInterval);
            
            yield return new WaitForSeconds(waveInterval);

            waveIntervalView.ResetFillAmount();
            
            CurrentWave.ActivateWave();
            
            waveTextView.DoMoveAnimation(_currentWaveIndex + 1);
        }

        public bool IsEndWave()
        {
            return CurrentWave.IsKillAllEnemy() && _currentWaveIndex + 1 == WaveMaxCount;
        }
    }
}
