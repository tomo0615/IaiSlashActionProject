//using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class GameEffectManager : SingletonMonoBehaviour<GameEffectManager>
{
    private bool isShaking;

    private Transform mainCamera;

    private EffectPool _effectPool;

    [SerializeField] private SlashEffect _slashEffectPrefab;

    [SerializeField] private Transform _myTransform;

    /*
    private Subject<Vector3> ememyPosition = new Subject<Vector3>();
    public IObservable<Vector3> OnPositionChanged { get { return ememyPosition; } }
    */
    private void Start()
    {
        mainCamera = Camera.main.transform;
        isShaking = false;


        _effectPool = new EffectPool(_myTransform, _slashEffectPrefab);

        //破棄されたときにPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => _effectPool.Dispose());
    }
    /*
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
    */

    public void OnGetScorePlayer(Vector3 position)
    {
        var gameObj = _effectPool.Rent();

        gameObj.PlayEffect(position)
            .Subscribe(__ =>
            {
                _effectPool.Return(gameObj);
            });
    }
}
