using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Tools;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Buttons
{
    public sealed class ExperienceView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TextMeshProUGUI _text;
        
        [Space]
        [SerializeField] private Image _fillImage;
        [SerializeField] private Sprite _completedSprite;
        [SerializeField] private Sprite _inProgressSprite;

        private ExperienceState _state;
        
        public void SetVisible(bool isVisible)
        {
            _root.SetActive(isVisible);
        }

        public void SetProgress(float progress)
        {
            _fillImage.fillAmount = progress;
        }

        public void UpdateText(string text)
        {
            _text.text = text;
        }
        
        public void SetState(ExperienceState state)
        {
            _state = state;
            
            _fillImage.sprite = state == ExperienceState.InProgress
                ? _inProgressSprite
                : _completedSprite;
        }
        
    }
    
    public sealed class CharacterPopUpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _userName;
        [SerializeField] private Image _icon;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private ExperienceView _experienceView;

        [Space] 
        [SerializeField] private StatView _statPrefab;
        [SerializeField] private CharacterStatsView _characterStats;

        [Space] 
        [SerializeField] private BaseButton _closeBtn;
        [SerializeField] private SimpleButton _levelUpBtn;

        private ICharacterPopUpPresenter _characterPopUpPresenter;
        private IDisposable _disposable;
        
        public void Show(ICharacterPopUpPresenter popUpPresenter)
        {
            _characterPopUpPresenter = popUpPresenter;
            _userName.text = popUpPresenter.Name;
            _icon.sprite = popUpPresenter.Icon;
            _level.text = popUpPresenter.GetCurrentLevel();
            _description.text = popUpPresenter.Description;
            _experienceView.UpdateText(popUpPresenter.GetExperience());

            foreach (var stat in _characterPopUpPresenter.Stats)
            {
               var view = Instantiate(_statPrefab, _characterStats.Container);
                _characterStats.AddStat(view);
                view.Init(stat);
            }

            _disposable =_characterPopUpPresenter.LevelUpCommand.BindTo(_levelUpBtn.Button);
            UpdateButtonState();
            
            _closeBtn.AddListener(Hide);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _characterStats.Clear();
            _closeBtn.RemoveListener(Hide);
            _disposable?.Dispose();
        }
        
        private void UpdateButtonState()
        {
            // var buttonState = _productPresenter.CanBuy.Value
            //     ? BuyButtonState.Available
            //     : BuyButtonState.Locked;
            // _button.SetState(buttonState);
        }
    }

    public sealed class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        
        private readonly List<StatView> _stats = new ();

        public Transform Container => _container;

        public void AddStat(StatView statView)
        {
            _stats.Add(statView);
        }

        public void Clear()
        {
            foreach (var stat in _stats)
            {
                stat.DestroyGO();
            }

            _stats.Clear();
        }
    }

    public sealed class StatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;

        public void Init(CharacterStat stat)
        {
            _name.text = stat.Name;
            _value.text = stat.GetValue();
        }

        public void UpdateValue(string value)
        {
            _value.text = value;
        }
    }

   
    public enum ExperienceState : byte
    {
        InProgress,
        Completed
    }
    
    [CreateAssetMenu(fileName = "CharacterCatalog", menuName = "Data/New CharacterCatalog")]
    public sealed class CharacterCatalog : ScriptableObject
    {
        public CharacterInfo[] CharacterInfos;

        public CharacterInfo GetCharacterInfo(string key)
        {
            return CharacterInfos.FirstOrDefault(i => i.UserInfo.Name == key);
        }
    }


    [Serializable]
    public sealed class CharacterInfo
    {
        public UserInfo UserInfo;
        public CharacterLevel CharacterLevel;
        public CharacterStat[] CharacterStats; 
    }
    
    [Serializable]
    public sealed class CharacterLevel
    {
        public int CurrentLevel = 1;
        public int CurrentExperience;
        
    }

    
    public sealed class CharacterPresenterFactory
    {
        //private readonly ProductBuyer _productBuyer;
        //private readonly MoneyStorage _moneyStorage;

        public CharacterPresenterFactory()
        {
            //_productBuyer = productBuyer;
            //_moneyStorage = moneyStorage;
        }

        public ICharacterPopUpPresenter Create(CharacterInfo characterInfo)
        {
            return new CharacterPopUpPresenter();
        }
    }
    
    // public sealed class CharacterStatsPresenter : ICharacterStatsPresenter
    // {
    //     public IReadOnlyList<CharacterStat> ProductPresenters => _presenters;
    //     private readonly List<CharacterStat> _presenters = new();
    //
    //     public ShopPopupPresenter(ProductCatalog productCatalog, ProductPresenterFactory productPresenterFactory)
    //     {
    //         var products = productCatalog.Products;
    //         for (int i = 0, count = products.Length; i < count; i++)
    //         {
    //             var product = products[i];
    //             var presenter = productPresenterFactory.Create(product); 
    //             _presenters.Add(presenter);
    //         }
    //     }
    // }
}