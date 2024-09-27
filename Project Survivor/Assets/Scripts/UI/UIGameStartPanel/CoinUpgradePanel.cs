/****************************************************************************
 * 2024.9 LP210300021
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel : UIElement, IController
	{
		private void Awake()
		{
			CoinUpgradeItemTemplate.Hide();

			CoinUpgradeSystem.OnCoinSystemUpgraded.Register((Refresh)).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Refresh();
			
			CloseButton.onClick.AddListener(this.Hide);
		}

		public void Refresh()
		{
			CoinUpgradeItemRoot.DestroyChildren();
			
			foreach (var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(e => e.ConditionCheck()))
			{
				CoinUpgradeItemTemplate.InstantiateWithParent(CoinUpgradeItemRoot)
					.Self(self =>
					{
						var itemCache = coinUpgradeItem;
						self.GetComponentInChildren<Text>().text = coinUpgradeItem.Description + $" {coinUpgradeItem.Price}金币";
						self.onClick.AddListener((() =>
						{
							itemCache.Upgrade();
						}));
						var selfCopy = self;
						Global.Coin.RegisterWithInitValue(coin =>
						{
							CoinLabel.text = "金币：" + coin;
							if (coin >= coinUpgradeItem.Price)
							{
								selfCopy.interactable = true;
							}
							else
							{
								selfCopy.interactable = false;
							}
						}).UnRegisterWhenGameObjectDestroyed(gameObject);
					})
					.Show();
			}
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