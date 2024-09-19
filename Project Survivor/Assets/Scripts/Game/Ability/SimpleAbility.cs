using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class SimpleAbility : ViewController
	{
		private float currentSec = 0.0f;

		public BindableProperty<float> damange = new BindableProperty<float>(1);
		public BindableProperty<float> frequent = new BindableProperty<float>(1.5f);

		void Start()
		{
			// Code Here
		}

		private void Update()
		{
			currentSec += Time.deltaTime;
			if (currentSec > frequent.Value)
			{
				currentSec = 0.0f;

				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

				foreach (Enemy item in enemies)
				{
					var distance = (item.transform.position - Player.Instance.transform.position).magnitude;
					//("distance = " + distance.ToString()).LogInfo();
					if (distance <= 5.0f)
					{
						ShinkObj(item);
					}
				}
				// var nearByEms = enemies.Where(t =>
				// {
				// 	var distance = (t.transform.position - Player.Instance.transform.position).magnitude;
				// 	("distance = " + distance.ToString()).LogInfo();
				// 	return distance <= 5.0f;
				// }).Select(t => ShinkObj(t));
			}
		}

		private Enemy ShinkObj(Enemy obj)
		{
			obj.Sprite.color = Color.red;
			var enemyRef = obj;
			ActionKit.Delay(0.3f, () =>
			{
				if (enemyRef)
				{
					enemyRef.HP-= damange.Value;
					enemyRef.Sprite.color = Color.white;
				}
			}).StartGlobal();
			return obj;
		}
	}
}
