using UnityEngine;
using UniRx.Toolkit;

public class EffectPool : ObjectPool<GameEffect>
{
    private readonly GameEffect _effect;

    private readonly Transform _transform;

    public EffectPool(Transform transform, GameEffect prefab)
    {
        _effect = prefab;
        _transform = transform;
    }

    //追加で生成されるときに実行
    protected override GameEffect CreateInstance()
    {
        var obj = GameObject.Instantiate(_effect);

        //ヒエラルキーが散らからないように一箇所にまとめる
        obj.transform.SetParent(_transform);

        return obj;
    }
}