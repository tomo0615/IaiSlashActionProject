using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class Waves : MonoBehaviour
{
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
            .Where(_ => CurrentWave.GetActiveEnemyValue() == 0 && HasNextWave)
           .Subscribe(_ => OnUpdateNextWave());
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

    private void OnUpdateNextWave()
    {
        //Waveの変更(ex.1->2)
        CurrentWave.gameObject.SetActive(false);

        currentWaveIndex++;

        CurrentWave.gameObject.SetActive(true);
    }
}
