using UnityEngine;

public class ExplosionEffect : GameEffect
{
    private ParticleSystem[] effects = new ParticleSystem[2];

    private void Awake()
    {
        effects = GetComponentsInChildren<ParticleSystem>();
    }
}
