/****************************************************************************
 * 2024.9 LP210300021
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel : UIElement, IController
	{
		private void Awake()
		{
			CoinPrecUpgrade.onClick.AddListener(() => {  });
			ExpPrecUpgrade.onClick.AddListener(() => { Global.ExpDropPrec.Value += 0.1f; });

			foreach (var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().mItems)
			{
				CoinUpgradeItemTemplate.InstantiateWithParent(CoinUpgradeItemRoot)
					.Self(self =>
					{
						var itemCache = coinUpgradeItem;
						self.GetComponentInChildren<Text>().text = coinUpgradeItem.Description;
						self.onClick.AddListener((() =>
						{
							itemCache.Upgrade();
						}));
					})
					.Show();
			}
			
			
			CloseButton.onClick.AddListener(() => { this.Hide(); });

			Global.Coin.RegisterWithInitValue(coin =>
			{
				CoinLabel.text = "金币：" + coin;
				if (coin >= 5)
				{
					CoinPrecUpgrade.Show();
					ExpPrecUpgrade.Show();
				}
				else
				{
					CoinPrecUpgrade.Hide();
					ExpPrecUpgrade.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}