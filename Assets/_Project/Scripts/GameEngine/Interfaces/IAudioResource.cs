using System.Collections.Generic;
using _Project.Scripts.GameEngine.Enums;
using UnityEngine;


namespace _Project.Scripts.GameEngine.Interfaces
{
    public interface IAudioResource
    {
        public AudioClip GetAudio(SfxType type);
        public IReadOnlyDictionary<SfxType, AudioClip> GetAllAudio();
    }
}