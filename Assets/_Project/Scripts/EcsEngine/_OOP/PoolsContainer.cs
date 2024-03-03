using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP
{
    public sealed class PoolsContainer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}