using System;

namespace ProjectSurvivor
{
    public class CoinUpgradeItem
    {
        public string Key { get; private set; }
        public string Description { get; private set; }
        
        public int Price { get; private set; }

        public bool UpgradeFinished { get; private set; } = false;

        public void Upgrade()
        {
            OnUpgrade?.Invoke(this);
            UpgradeFinished = true;
            CoinUpgradeSystem.OnCoinSystemUpgraded.Trigger();
        }

        public bool ConditionCheck()
        {
            if (Condition != null)
            {
                return !UpgradeFinished && Condition.Invoke(this);
            }

            return !UpgradeFinished;
        }
        
        public Action<CoinUpgradeItem> OnUpgrade { get; private set; }
        public Func<CoinUpgradeItem, bool> Condition { get; private set; }
        
        

        public CoinUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public CoinUpgradeItem WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public CoinUpgradeItem withPrice(int price)
        {
            Price = price;
            return this;
        }

        public CoinUpgradeItem WithOnUpgrade(Action<CoinUpgradeItem> onUpgrade)
        {
            OnUpgrade = onUpgrade;
            return this;
        }

        public CoinUpgradeItem WithCondition(Func<CoinUpgradeItem, bool> condition)
        {
            Condition = condition;
            return this;
        }
        
    }
}