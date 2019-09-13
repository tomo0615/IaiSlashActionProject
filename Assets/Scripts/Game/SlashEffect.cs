using System;
using UniRx;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField]private double finishTime = 1.0f;

    private ParticleSystem _slashEffect;

    private ParticleSystem Effect
    {
        get
        {
            return _slashEffect ?? (_slashEffect = GetComponent<ParticleSystem>());
        }
    }


    public IObservable<Unit> PlayEffect(Vector3 position)
    {
        transform.position = position;

        Effect.Play();

        return Observable.Timer(TimeSpan.FromSeconds(finishTime))
             .ForEachAsync(_ => Effect.Stop());
    }
}
