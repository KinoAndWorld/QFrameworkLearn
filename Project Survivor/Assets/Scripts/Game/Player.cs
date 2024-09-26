using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{

		public static Player Instance;

		public BindableProperty<int> HP = new BindableProperty<int>(10);

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
			float hor = Input.GetAxisRaw("Horizontal");
			float vel = Input.GetAxisRaw("Vertical");

			Vector2 direction = new Vector2(hor, vel).normalized;
			Vector2 lerpVel = direction * moveSpeed;
			var pow = 1.0f - Mathf.Exp(-Time.deltaTime * 20);
			SelfRigidbody.velocity = Vector2.Lerp(SelfRigidbody.velocity, lerpVel, pow);
			// ("log hor = " + hor.ToString() + ", vel = " + vel.ToString()).LogInfo();
			// ("direction = " + direction.ToString() + ", velocity = " + (direction * 10.0f).ToString()).LogInfo();
		}
	}
}
