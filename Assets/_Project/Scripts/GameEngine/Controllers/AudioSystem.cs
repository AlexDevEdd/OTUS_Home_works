using System;
using System.Collections.Generic;
using _Project.Scripts.GameEngine.Enums;
using _Project.Scripts.GameEngine.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class AudioSystem : IInitializable, IAudioSystem
    {
        private readonly Dictionary<SfxType, AudioClip> _clips = new ();
        private readonly IAudioResource _resources;

        [Inject]
        public AudioSystem(IAudioResource resources)
        {
            _resources = resources;
        }

        public void Initialize()
        {
            foreach (var clip in _resources.GetAllAudio())
            {
                _clips.TryAdd(clip.Key, clip.Value);
            }
        }

        public void PlayAudio(AudioSource source, SfxType type)
        {
            if(_clips.TryGetValue(type, out var clip))
                source.PlayOneShot(clip);
            else
                throw new ArgumentException($"Doesn't exist type of {type}");
            
        }
    }
}