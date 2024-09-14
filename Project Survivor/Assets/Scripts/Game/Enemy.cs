using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public int HP = 3;
		void Start()
		{
			// Code Here
		}

		private void Update() {
			if (Player.Instance) {
				Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
				transform.Translate(direction * Time.deltaTime * 2.0f);
			}
			if (HP <= 0)
			{
				this.DestroyGameObjGracefully();
				UIKit.OpenPanel<GamePassPanel>();

				Global.Exp.Value++;
			}
		}
	}
}
