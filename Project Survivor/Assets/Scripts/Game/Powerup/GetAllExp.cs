using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GetAllExp : ViewController
	{
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other) {
			var collectArea = other.GetComponent<CollectableArea>();
			if (collectArea) {
                //AudioKit.PlaySound("bomb");

                // 把主角半径5以内所有经验吸收
				foreach (var item in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, sortMode: FindObjectsSortMode.None) ) {
					var distance = (item.transform.position - Player.Instance.transform.position).magnitude;
					if (distance <= 5)
					{
						item.TrendObj(Player.Instance.gameObject);
					}
                }

				this.DestroyGameObjGracefully();
			}
		}
	}
}
