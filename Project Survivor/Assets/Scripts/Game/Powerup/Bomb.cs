using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Bomb : ViewController
	{
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other) {
			var collectArea = other.GetComponent<CollectableArea>();
			if (collectArea) {
                //AudioKit.PlaySound("bomb");

                // destory all enemies
                foreach (var item in FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, sortMode: FindObjectsSortMode.None) ) {
					item.Hurt(item.HP);
                }


				this.DestroyGameObjGracefully();
			}
		}
	}
}
