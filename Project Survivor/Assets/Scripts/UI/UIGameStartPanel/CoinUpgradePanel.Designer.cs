/****************************************************************************
 * 2024.9 LP210300021
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button CloseButton;
		[SerializeField] public UnityEngine.UI.Text CoinLabel;
		[SerializeField] public UnityEngine.UI.Button ExpPrecUpgrade;
		[SerializeField] public UnityEngine.UI.Button CoinPrecUpgrade;
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeItemTemplate;
		[SerializeField] public RectTransform CoinUpgradeItemRoot;

		public void Clear()
		{
			CloseButton = null;
			CoinLabel = null;
			ExpPrecUpgrade = null;
			CoinPrecUpgrade = null;
			CoinUpgradeItemTemplate = null;
			CoinUpgradeItemRoot = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
