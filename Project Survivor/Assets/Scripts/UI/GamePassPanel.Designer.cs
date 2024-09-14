using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:05f9762e-8309-4910-9022-28f5b474a0c4
	public partial class GamePassPanel
	{
		public const string Name = "GamePassPanel";
		
		
		private GamePassPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public GamePassPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePassPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePassPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
