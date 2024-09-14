using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{

		public static Player Instance;
		public float moveSpeed = 5.0f;

		private void Awake()
		{
			Instance = this;
		}

		private void OnDestroy() {
			Instance = null;
		}

		void Start()
		{
			Sprite.color = Color.green;
			HurtBox.OnTriggerEnter2DEvent(collider2D =>
			{
				UIKit.OpenPanel<GameOverPanel>();

				collider2D.transform.root.gameObject.DestroySelfGracefully();

				this.DestroyGameObjGracefully();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			float hor = Input.GetAxis("Horizontal");
			float vel = Input.GetAxis("Vertical");

			Vector2 direction = new Vector2(hor, vel).normalized;
			SelfRigidbody.velocity = direction * moveSpeed;

			// ("log hor = " + hor.ToString() + ", vel = " + vel.ToString()).LogInfo();
			// ("direction = " + direction.ToString() + ", velocity = " + (direction * 10.0f).ToString()).LogInfo();
		}
	}
}
