using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:7dd823e4-9ac0-4f42-965a-d9025874df9c
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text ExpLabel;
		[SerializeField]
		public UnityEngine.UI.Text CoinLabel;
		[SerializeField]
		public UnityEngine.UI.Text LevelLabel;
		[SerializeField]
		public UnityEngine.UI.Text TimeLabel;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountLabel;
		[SerializeField]
		public RectTransform UpgradeContainer;
		[SerializeField]
		public UnityEngine.UI.Button UpgradeButton;
		[SerializeField]
		public UnityEngine.UI.Button UpgradeAtkFreButton;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExpLabel = null;
			CoinLabel = null;
			LevelLabel = null;
			TimeLabel = null;
			EnemyCountLabel = null;
			UpgradeContainer = null;
			UpgradeButton = null;
			UpgradeAtkFreButton = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
