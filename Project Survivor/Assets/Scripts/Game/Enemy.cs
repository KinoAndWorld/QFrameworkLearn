using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public float HP = 3;
		void Start()
		{
			// Code Here
			EnemyGenerator.EmemyCount.Value++;
		}

        private void OnDestroy() {
			EnemyGenerator.EmemyCount.Value--;
		}

        private void Update() {
			if (Player.Instance) {
				Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
				transform.Translate(direction * Time.deltaTime * 2.0f);
			}
			if (HP <= 0)
			{
				// 死亡掉落
				Global.GeneratePowerUp(gameObject);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
