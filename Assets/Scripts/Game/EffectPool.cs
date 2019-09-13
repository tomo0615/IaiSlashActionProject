using UnityEngine;
using UniRx.Toolkit;

public class EffectPool : ObjectPool<SlashEffect>
{
    private readonly SlashEffect _slashEffect;

    private readonly Transform _transform;

    public EffectPool(Transform transform, SlashEffect prefab)
    {
        _slashEffect = prefab;
        _transform = transform;
    }

    //追加で生成されるときに実行
    protected override SlashEffect CreateInstance()
    {
        var obj = GameObject.Instantiate(_slashEffect);

        //ヒエラルキーが散らからないように一箇所にまとめる
        obj.transform.SetParent(_transform);

        return obj;
    }
}