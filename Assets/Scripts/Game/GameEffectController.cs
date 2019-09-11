using DG.Tweening;
using UnityEngine;

public class GameEffectController : SingletonMonoBehaviour<GameEffectController>
{
    private bool isShaking;

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        isShaking = false;
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
