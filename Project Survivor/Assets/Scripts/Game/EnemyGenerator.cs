using UnityEngine;
using QFramework;
using System;

namespace ProjectSurvivor
{
	public partial class EnemyGenerator : ViewController
	{
		private float _curSec = 0;

		void Start()
		{
			// Code Here
		}

		private void Update() {
			_curSec += Time.deltaTime;

			if (_curSec >= 1.0f)
			{
				_curSec = 0;

				// 随机方向生成敌人
				var randomAngle = UnityEngine.Random.Range(0, 360f);
				var randomRad = randomAngle * Mathf.Deg2Rad;
				var randomDirection = new Vector3(Mathf.Cos(randomRad), Mathf.Sin(randomRad));

				// var randomTest = UnityEngine.Random.insideUnitCircle;
				// var rrr = new Vector3(randomTest.x, randomTest.y);

				var genrPos = Player.Instance.transform.position + randomDirection * 10;
				// 生成敌人
				Enemy.Instantiate().Position(genrPos).Show();

			}
		}
	}
}
