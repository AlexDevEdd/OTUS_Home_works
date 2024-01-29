// using Atomic.Behaviours;
// using Atomic.Extensions;
// using GameEngine;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace Sample
// {
//     public sealed class RotationTest : MonoBehaviour
//     {
//         [SerializeField]
//         private AtomicBehaviour character;
//         
//         [Button]
//         public void AddRotation()
//         {
//             var isAlive = this.character.GetValue<bool>(ObjectAPI.IsAlive);
//             var moveDirection = this.character.GetValue<Vector3>(ObjectAPI.MoveDirection);
//             var rotationMechanics = new RotationMechanics(isAlive, moveDirection, this.character.transform);
//             this.character.AddLogic(rotationMechanics);
//         }
//
//         [Button]
//         public void RemoveRotation()
//         {
//             this.character.RemoveLogic<RotationMechanics>();
//         }
//     }
// }