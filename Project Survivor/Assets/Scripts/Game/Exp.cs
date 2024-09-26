using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{
		private GameObject trendObjAnchor;
		public int expValue = 1;
		void Start()
		{
			// Code Here
		}

		public void TrendObj(GameObject anchor) 
		{
			trendObjAnchor = anchor;
		}

		private void FixedUpdate() {
			if (trendObjAnchor)
			{
				Vector3 direction = (trendObjAnchor.transform.position - transform.position).normalized;
				transform.Translate(direction * Time.deltaTime * 6f);
			}
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
