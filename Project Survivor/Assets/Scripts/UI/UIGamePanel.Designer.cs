using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:6650c4de-a4e4-4c7c-839a-b8403c61e87b
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
		public UnityEngine.UI.Button UpgradeButton;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExpLabel = null;
			LevelLabel = null;
			TimeLabel = null;
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
