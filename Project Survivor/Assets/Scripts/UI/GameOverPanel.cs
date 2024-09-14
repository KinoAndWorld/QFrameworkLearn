using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class GameOverPanelData : UIPanelData
	{
	}
	public partial class GameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GameOverPanelData ?? new GameOverPanelData();
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					SceneManager.LoadScene("SampleScene");
					this.CloseSelf();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnOpen(IUIData uiData = null)
		{
		}

		protected override void OnShow()
		{
		}

		protected override void OnHide()
		{
		}

		protected override void OnClose()
		{
		}
	}
}
