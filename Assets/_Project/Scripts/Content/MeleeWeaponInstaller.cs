using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.Content
{
    public class MeleeWeaponInstaller : EntityInstaller
    {
        protected override void Install(Entity entity)
        {
            entity.AddData(new MeleeWeapon());
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}