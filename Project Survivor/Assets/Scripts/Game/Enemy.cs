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

        private void FixedUpdate() {
			if (Player.Instance) {
				Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
				//transform.Translate(direction * Time.deltaTime * 1.5f);
				SelfRigidbody2D.velocity = direction * 1.5f;
			} else {
				SelfRigidbody2D.velocity = Vector2.zero;
			}
		}

		public void Hurt(float damange) {
			Sprite.color = Color.red;
			var enemyRef = this;
			AudioKit.PlaySound("hit");

			FloatTextController.Play(transform.position, damange.ToString());

			ActionKit.Delay(0.2f, () =>
			{
				if (enemyRef) {
					enemyRef.HP -= damange;
					enemyRef.Sprite.color = Color.white;
				}
			}).Start(this);
		}

        private void Update() {
			if (HP <= 0) {
				// 死亡掉落
				Global.GeneratePowerUp(gameObject);
				this.DestroyGameObjGracefully();
			}
		}
	}
}
