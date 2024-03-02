using System;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class CollisionTest : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TRIGGER");
        }
    }
}