using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private void Awake()
        {
            InitializeWaveList();
        }

        void Start()
        {
            OnStartWave();

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

        public void OnStartWave()
        {
            CurrentWave.gameObject.SetActive(true);
        }

        private IEnumerator OnUpdateNextWave()
        {
            //Waveの変更(ex.1->2)
            CurrentWave.gameObject.SetActive(false);

            currentWaveIndex++;

            yield return new WaitForSeconds(waveInterval);

            CurrentWave.gameObject.SetActive(true);
        }
    }
}
