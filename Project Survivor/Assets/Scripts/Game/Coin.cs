using UnityEngine;
using QFramework;
using MoonSharp.VsCodeDebugger.SDK;

namespace ProjectSurvivor
{
	public partial class Coin : ViewController
	{
		public int coinValue = 1;
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var collectArea = other.GetComponent<CollectableArea>();
			if (collectArea)
			{
				AudioKit.PlaySound("coin");
				Global.Coin.Value += coinValue;
				this.DestroyGameObjGracefully();	
			}
		}
	}
}
