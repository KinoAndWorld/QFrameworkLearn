using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:eb96243a-784c-412e-96a1-b0cd64d6f670
	public partial class GameOverPanel
	{
		public const string Name = "GameOverPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BackToHomeButton;
		
		private GameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BackToHomeButton = null;
			
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
