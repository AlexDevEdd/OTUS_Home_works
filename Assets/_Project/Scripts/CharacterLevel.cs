using System;
using System.Collections.Generic;
using System.IO;
using _Project.Scripts.UI.Buttons;
using UniRx;
using UnityEngine;

namespace _Project.Scripts
{
    
    public interface ICharacterPopUpPresenter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }
        public ReactiveProperty<int> CurrentLevel { get; }
        public ReactiveProperty<int> CurrentExperience { get; }
        public HashSet<CharacterStat> Stats { get; }
        public int GetRequiredExperience();
        public string GetCurrentLevel();
        public string GetExperience();
        ReactiveCommand LevelUpCommand { get; }

    }

    public sealed class CharacterPopUpPresenter : ICharacterPopUpPresenter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }
        public ReactiveProperty<int> CurrentLevel { get; private set; }
        public ReactiveProperty<int> CurrentExperience { get; private set;}
        public HashSet<CharacterStat> Stats { get; private set; }
        
        public void AddExperience(int range)
        {
            // var xp = Math.Min(CurrentExperience + range, RequiredExperience);
            // CurrentExperience = xp;
            // OnExperienceChanged?.Invoke(xp);
        }
        
        public void LevelUp()
        {
            if (CanLevelUp())
            {
                // CurrentExperience = 0;
                // CurrentLevel++;
                // OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return true;
            //return CurrentExperience == RequiredExperience;
        }

        
        
        public int GetRequiredExperience()
        {
            return 100 * (CurrentLevel.Value +1);
        }

        public string GetCurrentLevel()
        {
            throw new NotImplementedException();
        }

        public string GetExperience()
        {
            throw new NotImplementedException();
        }

        public ReactiveCommand LevelUpCommand { get; }
    }

    public sealed class CharacterLevelSystem
    {
        private Dictionary<string, CharacterLevel> _characterLevels;
        
        public int GetRequiredExperience(string characterKey)
        {
            if (_characterLevels.TryGetValue(characterKey, out var value))
            {
                return 100 * (value.CurrentLevel + 1);
            }

            throw new InvalidDataException($"does not exist character with key {nameof(characterKey)}");
        }
    }
    
   
}