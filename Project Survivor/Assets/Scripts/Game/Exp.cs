using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{

		public int expValue = 1;
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var collectArea = other.GetComponent<CollectableArea>();
			if (collectArea)
			{
				AudioKit.PlaySound("exp");
				Global.Exp.Value += expValue;
				this.DestroyGameObjGracefully();	
			}
		}
	}
}
