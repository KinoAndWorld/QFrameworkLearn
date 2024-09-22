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

			Time.timeScale = 0;
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Global.ResetData();
					Time.timeScale = 1;
					SceneManager.LoadScene("Game");
					this.CloseSelf();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			BackToHomeButton.onClick.AddListener(() => {
				this.CloseSelf();
				Time.timeScale = 1;
				SceneManager.LoadScene("GameStart");
			});
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
