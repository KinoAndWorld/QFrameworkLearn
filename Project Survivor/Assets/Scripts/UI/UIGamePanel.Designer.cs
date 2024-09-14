using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:27b2f2ea-e4c5-4da8-81af-3a9b07bd852b
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text ExpLabel;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExpLabel = null;
			
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
