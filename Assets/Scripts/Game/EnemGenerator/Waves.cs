using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class Waves : MonoBehaviour
{
    private List<WaveTest> waveList = new List<WaveTest>();

    private int currentWaveIndex = 0;

    public WaveTest CurrentWave() => waveList[currentWaveIndex];

    public int WaveMaxCount() => waveList.Count;

    public bool HasNextWave() => currentWaveIndex + 1 < WaveMaxCount();

    private void Awake()
    {
        InitializeWaveList();
    }

    void Start()
    {
        OnStartWave();

        //Wave更新用
        this.UpdateAsObservable()
            .Where(_ => CurrentWave().GetActiveEnemyValue() == 0)
           .Subscribe(_ => OnUpdateNextWave());
    }

    private void InitializeWaveList()
    {
        waveList = GetComponentsInChildren<WaveTest>().Select(
            (wave, index) =>
            {
                wave.InitializeWave();
                wave.gameObject.SetActive(false);
                return wave;
            }).ToList();
    }

    public void OnStartWave()
    {
        CurrentWave().gameObject.SetActive(true);
    }

    private void OnUpdateNextWave()
    {
        if (!HasNextWave()) return;//GameClear!

        //Waveの変更(ex.1->2)
        CurrentWave().gameObject.SetActive(false);

        currentWaveIndex++;
        CurrentWave().gameObject.SetActive(true);
    }
}
