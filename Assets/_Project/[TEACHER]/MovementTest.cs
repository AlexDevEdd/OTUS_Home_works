// using Atomic.Behaviours;
// using Atomic.Extensions;
// using Atomic.Objects;
// using GameEngine;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace Sample
// {
//     public sealed class MovementTest : MonoBehaviour
//     {
//         [SerializeField]
//         private AtomicBehaviour character;
//
//         [SerializeField]
//         private float speed = 5;
//
//         [Button]
//         public void AddMovement()
//         {
//             var movementComponent = new MoveComponent(this.character.transform, this.speed);
//             this.character.AddLogic(movementComponent);
//             this.character.AddComponent(movementComponent);
//
//             var animator = this.character.Get<Animator>(ObjectAPI.Animator);
//             if (animator != null)
//             {
//                 var animatorController = new MoveAnimatorController(animator, movementComponent.IsMoving);
//                 this.character.AddLogic(animatorController);
//             }
//             
//             var isAlive = this.character.GetValue<bool>(ObjectAPI.IsAlive);
//             var stateController = new UpdateMechanics(() => movementComponent.Enabled.Value = isAlive.Value);
//             this.character.AddLogic("MoveStateController", stateController);
//         }
//
//         [Button]
//         public void RemoveMovement()
//         {
//             this.character.RemoveComponent<MoveComponent>();
//             this.character.RemoveLogic<MoveComponent>();
//             this.character.RemoveLogic<MoveAnimatorController>();
//             this.character.RemoveLogic("MoveStateController");
//         }
//     }
// }