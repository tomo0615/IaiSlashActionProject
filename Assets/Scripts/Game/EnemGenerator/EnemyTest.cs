using UnityEngine;

public class EnemyTest : MonoBehaviour, IAttackable
{
    private void Start()
    {
        //出現effect
        
    }

    public void Attacked()
    {
        //爆発エフェクト

        gameObject.SetActive(false);
    }
}
