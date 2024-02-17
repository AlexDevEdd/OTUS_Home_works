﻿using System;
using System.Collections.Generic;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.ScriptableConfigs
{
    [Serializable]
    public class GameBalance : ICustomInject
    {
        [Title("Unit configurations", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] 
        private SerializableDictionary<UnitType, UnitConfig> _unitConfig;
        
        [Title("Vfx Factory configurations", TitleAlignment = TitleAlignments.Centered)]
        [Space, SerializeField] 
        private SerializableDictionary<VfxType, VfxFactoryConfig> _vfxFactoryConfigs;
        
        public UnitConfig GetUnitConfigByClass(UnitType type)
        {
            if(_unitConfig.TryGetValue(type, out var config))
            {
                return config;
            }

            throw new ArgumentException($"Config with type of {type} doesn't exist");
        }
        
        public VfxFactoryConfig GetVfxFactoryConfigByType(VfxType type)
        {
            if(_vfxFactoryConfigs.TryGetValue(type, out var config))
            {
                return config;
            }

            throw new ArgumentException($"Config with type of {type} doesn't exist");
        }
    }
    
    [Serializable]
    public struct UnitConfig
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public int Health;
    }
    
    [Serializable]
    public struct VfxFactoryConfig
    {
        public VfxType Type;
        public string PrefabKey;
        public int PoolSize;
    }
}