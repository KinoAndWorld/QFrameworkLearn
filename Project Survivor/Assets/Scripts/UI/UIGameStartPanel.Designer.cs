using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:8db93d93-d1bf-4387-968d-a528143d9ae0
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button CoinUpgradeButton;
		[SerializeField]
		public UnityEngine.UI.Button StartGameButton;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Button CoinPrecUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button ExpPrecUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		[SerializeField]
		public UnityEngine.UI.Text CoinLabel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CoinUpgradeButton = null;
			StartGameButton = null;
			CoinUpgradePanel = null;
			CoinPrecUpgrade = null;
			ExpPrecUpgrade = null;
			CloseButton = null;
			CoinLabel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
