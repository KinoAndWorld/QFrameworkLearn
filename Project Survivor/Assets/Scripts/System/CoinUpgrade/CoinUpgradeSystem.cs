using System.Collections.Generic;
using QFramework;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem: AbstractSystem
    {
        public List<CoinUpgradeItem> mItems = new List<CoinUpgradeItem>();
        
        protected override void OnInit()
        {
            mItems.Add(new CoinUpgradeItem()
                .WithKey("coin_precent")
                .WithDescription("金币掉落概率提升")
                .WithOnUpgrade(() =>
                {
                    Global.CoinDropPrec.Value += 0.1f;
                    Global.Coin.Value -= 5;
                }
            ));
            
            mItems.Add(new CoinUpgradeItem()
                .WithKey("Coin2222")
                .WithDescription("4554")
                .WithOnUpgrade(() =>
                {
                    
                }
            ));
        }

        public void Say()
        {
            "Hello".LogInfo();
        }
    }
}