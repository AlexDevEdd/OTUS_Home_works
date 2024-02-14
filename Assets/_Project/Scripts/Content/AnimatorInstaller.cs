using _Project.Scripts.EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class AnimatorInstaller : EntityInstaller
    {
        [SerializeField] private Animator _animator;
        protected override void Install(Entity entity)
        {
            entity.AddData(new AnimatorView{Value = _animator});
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}