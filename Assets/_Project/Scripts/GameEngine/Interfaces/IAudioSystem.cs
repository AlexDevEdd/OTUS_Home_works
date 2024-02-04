using _Project.Scripts.GameEngine.Enums;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Interfaces
{
    public interface IAudioSystem
    {
        public void PlayAudio(AudioSource source, SfxType type);
    }
}