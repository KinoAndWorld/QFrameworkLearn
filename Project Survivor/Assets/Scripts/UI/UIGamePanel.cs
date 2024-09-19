using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public class UIGamePanelData : UIPanelData
	{
	}
	public partial class UIGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();


			/// 时间
			Global.CurrentTime.RegisterWithInitValue(value => {
                if (Time.frameCount % 30 == 0) {
					var curSecs = Mathf.FloorToInt(value);
					var min = curSecs / 60;
					var sec = curSecs % 60;
					TimeLabel.text = "时间：" + $"{min:00}:{sec:00}";
				}
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			var enemyGenarator = FindObjectOfType<EnemyGenerator>();

			ActionKit.OnUpdate.Register(() => {
				Global.CurrentTime.Value += Time.deltaTime;
                if (enemyGenarator.WaveFinish &&
					EnemyGenerator.EmemyCount.Value == 0) {

					UIKit.OpenPanel<GamePassPanel>();
					Global.ResetData();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			// 经验值
			Global.Exp.RegisterWithInitValue(value => {
				ExpLabel.text = "经验值：" + value;

                if (value >= 5 ) {
					Global.Exp.Value -= 5;
					Global.Level.Value++;
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.RegisterWithInitValue(value => {
				LevelLabel.text = "等级：" + value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			Global.Level.Register(value => {
				UpgradeContainer.Show();
				Time.timeScale = 0;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			UpgradeContainer.Hide();
			UpgradeButton.onClick.AddListener(() => {
				Time.timeScale = 1;
				UpgradeContainer.Hide();

				Player.Instance.simpleAbility.damange.Value *= 1.5f;
			});
			UpgradeAtkFreButton.onClick.AddListener(() => {
				Time.timeScale = 1;
				UpgradeContainer.Hide();

				Player.Instance.simpleAbility.frequent.Value *= 0.8f;
			});

			// 敌人数量
			EnemyGenerator.EmemyCount.RegisterWithInitValue(value => {
				EnemyCountLabel.text = "敌人数量：" + value;
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
