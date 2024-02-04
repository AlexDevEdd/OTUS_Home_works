using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.GameEngine.Enums;
using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Tools.Serialize;
using Sirenix.OdinInspector;
using UnityEngine;


namespace _Project.Scripts.DI.Installers.ScriptableObjects
{
    [Serializable]
    public class GameResources : IAudioResource
    {
        [SerializeField] private List<Sprite> _icons;
        
        [Title("Audio References", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField]private SerializableDictionary<SfxType, AudioClip> _audio;
        
        
        public AudioClip GetAudio(SfxType type)
        {
            if (_audio.TryGetValue(type, out var audioClip))
                return audioClip;
        
            throw new ArgumentException($"Doesn't exist KEY of {type}");
        }
        
        public IReadOnlyDictionary<SfxType, AudioClip> GetAllAudio()
        {
            return _audio;
        }
        
        public bool TryGetSprite(string key, out Sprite sprite)
        {
            sprite = _icons.FirstOrDefault(s => s.name == key);
            if (sprite != null)
                return true;

            throw new ArgumentException($"doesn't exist Sprite key of {key}");
        }
        
        public bool TryGetSprite<T>(T type, out Sprite sprite) where T: Enum
        {
            sprite = _icons.FirstOrDefault(s => s.name.Equals(type.ToString()));
            if (sprite != null)
                return true;

            throw new ArgumentException($"doesn't exist Sprite key of {type}");
        }
    }
}