using System.Collections.Generic;
using System.Save;
using QFramework;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem: AbstractSystem, ISaveable
    {
        public static EasyEvent OnCoinSystemUpgraded = new EasyEvent();
        
        public List<CoinUpgradeItem> Items = new List<CoinUpgradeItem>();
        
        protected override void OnInit()
        {
            Refresh();
            
        }

        public void Refresh()
        {
            var coinLv1 = new CoinUpgradeItem()
                .WithKey("coin_precent_lv1")
                .WithDescription("金币掉落概率提升 lv1")
                .withPrice(5)
                .WithOnUpgrade((item) =>
                    {
                        Global.CoinDropPrec.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }
                );
            var coinLv2 = new CoinUpgradeItem()
                .WithKey("coin_precent_lv2")
                .WithDescription("金币掉落概率提升 lv2")
                .withPrice(10)
                .WithCondition((_)=> coinLv1.UpgradeFinished)
                .WithOnUpgrade((item) =>
                    {
                        Global.CoinDropPrec.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }
                );
            
            Items.Add(coinLv1);
            Items.Add(coinLv2);
            
            Items.Add(new CoinUpgradeItem()
                .WithKey("exp_precent")
                .WithDescription("经验值掉落概率提升")
                .withPrice(3)
                .WithOnUpgrade(item =>
                    {
                        Global.ExpDropPrec.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }
                ));
            
            Items.Add(new CoinUpgradeItem()
                .WithKey("max_hp")
                .WithDescription("最大hp提升")
                .withPrice(8)
                .WithOnUpgrade(item =>
                    { 
                        Player.Instance.MaxHP.Value += 5;
                        Global.Coin.Value -= item.Price;
                    }
                ));
        }

        public void Say()
        {
            "Hello".LogInfo();
        }

        public void Save()
        {
            
        }

        public void Load()
        {
            
        }
    }
}