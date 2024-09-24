using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{

		public static Player Instance;

		public BindableProperty<int> HP = new BindableProperty<int>(5);

		public float moveSpeed = 5.0f;

		public SimpleAbility simpleAbility;

		private void Awake()
		{
			Instance = this;
			simpleAbility = FindObjectOfType<SimpleAbility>();

		}

		private void OnDestroy() {
			Instance = null;
		}

		void Start()
		{
			//Sprite.color = Color.green;
			HurtBox.OnTriggerEnter2DEvent(collider2D =>
			{
				var hitBox = collider2D.GetComponent<HitBox>();
				if (hitBox && hitBox.Owner.CompareTag("Enemy"))
				{
					"被击中，受伤".LogInfo();

					HP.Value -= 1;
                    if (HP.Value <= 0) {
						UIKit.OpenPanel<GameOverPanel>();
						collider2D.transform.root.gameObject.DestroySelfGracefully();
						Global.ResetData();
						this.DestroyGameObjGracefully();
					}
				}
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
