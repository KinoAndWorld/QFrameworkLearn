using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:53b7077f-111c-4fa3-a00e-608f55d5beb3
	public partial class GameOverPanel
	{
		public const string Name = "GameOverPanel";
		
		
		private GameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public GameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
