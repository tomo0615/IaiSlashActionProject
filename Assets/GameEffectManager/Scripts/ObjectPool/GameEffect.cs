using System;
using UniRx;
using UnityEngine;

public class GameEffect : MonoBehaviour
{
    [SerializeField, Header("再生時間")]
    private double finishTime = 1.0f;

    private ParticleSystem effect { get; set; }

    private ParticleSystem Effect
    {
        get
        {
            return effect ?? (effect = GetComponent<ParticleSystem>());
        }
    }

    public IObservable<Unit> PlayEffect(Vector3 position)
    {
        transform.position = position;

        Effect.Play();

        return Observable
            .Timer(TimeSpan.FromSeconds(finishTime))
            .ForEachAsync(_ => Effect.Stop());
    }
}