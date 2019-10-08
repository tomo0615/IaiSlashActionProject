using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections.Generic;
using System;

public enum EffectType //列挙型のeffectの名前を同じにする
{                      //Effect名をここに追加
    Explosion,
    Slash
}

public class GameEffectManager : SingletonMonoBehaviour<GameEffectManager>
{
    #region particle用
    private Dictionary<EffectType ,EffectPool> _effectPool = new Dictionary<EffectType, EffectPool>();

    [SerializeField] private List<GameEffect> effectList = new List<GameEffect>();
    #endregion
    private Transform _myTransform;

    private bool isShaking;

    private Transform mainCamera;

    private void Start()
    {
        _myTransform = GetComponent<Transform>();

        mainCamera = Camera.main.transform;
        isShaking = false;

        InitializeEffectList();
    }

    private void InitializeEffectList()
    {
        //すべてのエフェクトをディクショナリに格納
        foreach (var effect in effectList)
        {
            var name = effect.name;
            var type = (EffectType)Enum.Parse(typeof(EffectType), name);

            _effectPool.Add(type, new EffectPool(_myTransform, effect));
        }

        //オブジェクトが破棄されたときにプールを破棄できるようにする
        foreach (var value in _effectPool.Values)
        {
            this.OnDestroyAsObservable().Subscribe(_ => value.Dispose());
        }
    }
    public void OnGenelateEffect(Vector3 position, EffectType type)
    {
        //poolから借りて終わったら返す
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

