using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.Content
{
    public class UnitSpawnerInstaller : EntityInstaller
    {
       
        protected override void Install(Entity entity)
        {
            entity.AddData(new SpawnerTag());
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}