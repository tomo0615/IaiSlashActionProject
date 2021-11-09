using UnityEngine;
using Zenject;

public class InGameInstaller : MonoInstaller
{
    [SerializeField]
    private WeaponManager _weaponManager= default;

    public override void InstallBindings()
    {
        Container.Bind<WeaponManager>()
            .FromComponentInNewPrefab(_weaponManager).AsSingle();
    }
}