using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "WeaponInstaller", menuName = "Installers/WeaponInstaller")]
public class WeaponInstaller : ScriptableObjectInstaller<WeaponInstaller>
{
    [SerializeField] private WeaponTable _weapontable = default;
     public override void InstallBindings()
    {
        Container.BindInstance(_weapontable);
    }
}
