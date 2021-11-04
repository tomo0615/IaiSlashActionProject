using Game.Interface;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using DamageDefine;

namespace Game.Player
{
    public class AttackObject : MonoBehaviour
    {
        //ここで現在装備している武器の情報を取れるように

        public void Initialize()
        {
            //仮でDamageParam初期化
            DamageParam damageParam;
            damageParam.damageType = DamageType.NONE;
            damageParam.damageValue = 10;
            damageParam.level = 0;

            this.OnTriggerEnterAsObservable()
                .Select(damageable => damageable.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.TakeDamage(damageParam);
                    
                    //GameEffectManager.Instance.ShakeCamera(0.25f);

                    GameEffectManager.Instance.OnGenelateEffect(
                        transform.position,
                        EffectType.Slash);
                });
            
            gameObject.SetActive(false);
        }
    }
}
