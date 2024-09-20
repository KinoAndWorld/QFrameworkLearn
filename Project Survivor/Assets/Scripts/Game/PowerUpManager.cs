using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class PowerUpManager : ViewController
	{
		public static PowerUpManager Instance;

		private void Awake() {
			if (!Instance)
			{
				Instance = this;
			}
		}

		private void OnDestroy() {
			Instance = null;
		}

		void Start()
		{
			// Code Here
		}
	}
}
