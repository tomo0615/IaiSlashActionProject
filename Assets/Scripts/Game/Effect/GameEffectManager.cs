using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;
using System;

public class GameEffectManager : SingletonMonoBehaviour<GameEffectManager>
{
    //列挙型のeffectの名前を同じにする
    public enum EffectType
    {
        Explosion,
        Slash
    }

    private Dictionary<EffectType ,EffectPool> _effectPool = new Dictionary<EffectType, EffectPool>();

    [SerializeField] private List<GameEffect> effectList = new List<GameEffect>();

    private Transform _myTransform;

    private bool isShaking;

    private Transform mainCamera;

    private void Start()
    {
        _myTransform = GetComponent<Transform>();

        mainCamera = Camera.main.transform;
        isShaking = false;

        //すべてのエフェクトをディクショナリに格納
        foreach (var effect in effectList)
        {
            var name = effect.name;
            var type = (EffectType)Enum.Parse(typeof(EffectType), name);

            _effectPool.Add(type, new EffectPool(_myTransform, effect));
        }

        //オブジェクトが破棄されたときにプールを破棄できるようにする
        foreach(var value in _effectPool.Values)
        {
            this.OnDestroyAsObservable().Subscribe(_ => value.Dispose());
        }
    }

    public void OnGenelateEffect(Vector3 position, EffectType type)
    {
        var gameObj = _effectPool[type].Rent();

        gameObj.PlayEffect(position)
            .Subscribe(__ =>
            {
                _effectPool[type].Return(gameObj);
            });
    }

    public void ShakeCamera(float time)
    {
        if (isShaking) return;
        isShaking = true;

        mainCamera.DOShakePosition(time)
            .OnComplete(() =>
            {
                isShaking = false;
            });
    }
}

