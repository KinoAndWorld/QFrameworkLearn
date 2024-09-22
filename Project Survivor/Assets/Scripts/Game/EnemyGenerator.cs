using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;

namespace ProjectSurvivor
{
	[Serializable]
	public class EnemyWave {
		public float generateDur = 1;
		public GameObject enemyPrefab;
		public int seconds = 10;
    }

	public partial class EnemyGenerator : ViewController
	{
		private float _curGenerateSecs = 0;
		private float _curWaveSecs = 0;

		public int CurrentWave = 0;
		public bool WaveFinish = false;
		public static BindableProperty<int> EmemyCount = new BindableProperty<int>(0);


		[SerializeField]
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();

		private Queue<EnemyWave> enemyWaveQueue = new Queue<EnemyWave>();
		private EnemyWave curWave;


		void Start()
		{
            foreach (var item in enemyWaves) {
				enemyWaveQueue.Enqueue(item);
			}
		}

		private void Update() {

            if (curWave == null) {
                if (enemyWaveQueue.Count > 0) {
					curWave = enemyWaveQueue.Dequeue();
					_curGenerateSecs = 0;
					_curWaveSecs = 0;
					"进入下一波".LogInfo();
					CurrentWave++;
				} else {
					WaveFinish = true;
				}
            } else {
				_curGenerateSecs += Time.deltaTime;
				_curWaveSecs += Time.deltaTime;

                if (_curGenerateSecs >= curWave.generateDur) {
					_curGenerateSecs = 0;
					
					// var randomTest = UnityEngine.Random.insideUnitCircle;
					// var rrr = new Vector3(randomTest.x, randomTest.y);
					if (Player.Instance) {
						// 随机方向生成敌人
						var randomAngle = UnityEngine.Random.Range(0, 360f);
						var randomRad = randomAngle * Mathf.Deg2Rad;
						var randomDirection = new Vector3(Mathf.Cos(randomRad), Mathf.Sin(randomRad));
						var genrPos = Player.Instance.transform.position + randomDirection * 10;
						// 生成敌人
						curWave.enemyPrefab.Instantiate().Position(genrPos).Show();
					}
				}

                if (_curWaveSecs >= curWave.seconds) {
					curWave = null;
                }
			}
		}
	}
}
