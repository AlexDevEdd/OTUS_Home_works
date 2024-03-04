using _Game.Scripts.Tools;
using _Project.Scripts.EcsEngine;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.Content
{
    public sealed class InteractiveEntityComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Light _light;
        
        private Entity _entity;
        private EcsAdmin _ecsAdmin;
        
        [Inject]
        public void Construct(EcsAdmin ecsAdmin)
        {
            _ecsAdmin = ecsAdmin;
        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        // if (other.gameObject.TryGetComponent(out Entity target))
        // {
        //     if(target.HasData<Inactive>()) return;
        //     
        //     _ecsAdmin.CreateEntity(EcsWorlds.Events)
        //         .Add(new CollisionEnterRequest
        //         {
        //             SourceId = _entity.Id,
        //             TargetId = target.Id,
        //             ContactPosition = other.ClosestPoint(target.transform.position)
        //         });
        //
        //     if(target.HasData<BulletTag>() && !target.HasData<Collided>())
        //         target.AddData(new Collided());
        // }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _light.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _light.enabled = false;
        }
    }
}