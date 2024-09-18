using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:2eefb1bd-2d95-4aa3-8078-47a686fbe1ca
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text ExpLabel;
		[SerializeField]
		public UnityEngine.UI.Text LevelLabel;
		[SerializeField]
		public UnityEngine.UI.Text TimeLabel;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountLabel;
		[SerializeField]
		public UnityEngine.UI.Button UpgradeButton;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExpLabel = null;
			LevelLabel = null;
			TimeLabel = null;
			EnemyCountLabel = null;
			UpgradeButton = null;
			
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
