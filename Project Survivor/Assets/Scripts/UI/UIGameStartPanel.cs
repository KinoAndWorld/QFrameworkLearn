using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			// please add init code here


			StartGameButton.onClick.AddListener(() => {
				this.CloseSelf();

				SceneManager.LoadScene("Game");
				Global.ResetData();
			});


			CoinUpgradeButton.onClick.AddListener(() => {
				CoinUpgradePanel.Show();
			});

			CoinPrecUpgrade.onClick.AddListener(() => {
				Global.CoinDropPrec.Value += 0.1f;
			});
			ExpPrecUpgrade.onClick.AddListener(() => {
				Global.ExpDropPrec.Value += 0.1f;
			});

			CloseButton.onClick.AddListener(() => {
				CoinUpgradePanel.Hide();
			});

			Global.Coin.RegisterWithInitValue(coin => {
				CoinLabel.text = "½ð±Ò£º" + coin;
                if (coin >= 5) {
					CoinPrecUpgrade.Show();
					ExpPrecUpgrade.Show();
                } else {
					CoinPrecUpgrade.Hide();
					ExpPrecUpgrade.Hide();
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
