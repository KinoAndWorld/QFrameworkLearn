using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public float HP = 3;


        private void Awake() {
			// Code Here
			EnemyGenerator.EmemyCount.Value++;
		}

        void Start()
		{
			
		}

        private void OnDestroy() {
			if (EnemyGenerator.EmemyCount.Value > 0) {
				EnemyGenerator.EmemyCount.Value--;
			}
		}

        private void Update() {
			if (Player.Instance) {
				Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
				transform.Translate(direction * Time.deltaTime * 1.5f);
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
