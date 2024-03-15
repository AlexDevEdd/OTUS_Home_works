using System;
using _Game.Scripts.Tools;
using _Project.Scripts.ScriptableConfigs;
using JetBrains.Annotations;

namespace _Project.Scripts
{
    [UsedImplicitly]
    public sealed class MoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;
        
        private int _money;
        public int Money => _money;
        
        public MoneyStorage(GameBalance balance)
        {
            SetupMoney(balance.StartMoney);
        }
      
        public void EarnMoney(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not earn negative money {amount}");
            }

            var previousValue = _money;
            var newValue = previousValue + amount;

            _money = newValue;
            OnMoneyChanged?.Invoke(newValue);
            OnMoneyEarned?.Invoke(amount);
            Log.ColorLogDebugOnly($"_money added {Money}", ColorType.Orange);
        }
        
        public void SpendMoney(int amount)
        {
            if (amount == 0)
            {
                return;
            }

            if (amount < 0)
            {
                throw new Exception($"Can not spend negative money {amount}");
            }

            var previousValue = _money;
            var newValue = previousValue - amount;
            if (newValue < 0)
            {
                throw new Exception(
                    $"Negative money after spend. Money in bank: {previousValue}, spend amount {amount} ");
            }

            _money = newValue;
            OnMoneyChanged?.Invoke(newValue);
            OnMoneySpent?.Invoke(amount);
        }
        
        private void SetupMoney(int money)
        {
            _money = money;
            OnMoneyChanged?.Invoke(money);
            Log.ColorLogDebugOnly($"SetupMoney {Money}", ColorType.Orange);
        }

        public bool CanSpendMoney(int amount)
        {
            return _money >= amount;
        }
    }
}