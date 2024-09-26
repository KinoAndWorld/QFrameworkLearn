using System;

namespace ProjectSurvivor
{
    public class CoinUpgradeItem
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public void Upgrade()
        {
            OnUpgrade?.Invoke();
        }
        
        public Action OnUpgrade { get; private set; }

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

        public CoinUpgradeItem WithOnUpgrade(Action onUpgrade)
        {
            OnUpgrade = onUpgrade;
            return this;
        }
        
    }
}