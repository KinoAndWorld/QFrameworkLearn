using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CameraController : ViewController
	{
		private Vector2 targetPosition = Vector2.zero;
		private Vector3 currentCameraPos;
		private bool needShake = false;
		private int shakeFrame = 0;

		public static CameraController Instance;

		private void Awake()
		{
			Instance = this;
		}

		private void OnDestroy()
		{
			Instance = null;
		}

		void Start()
		{
			Application.targetFrameRate = 60;
		}

		public static void Shake()
		{
			Instance.needShake = true;
			Instance.shakeFrame = 30;
		}

		private void Update()
		{
			if (Player.Instance)
			{
				targetPosition = Player.Instance.transform.position;
				var pow = 1.0f - Mathf.Exp(-Time.deltaTime * 20);

				currentCameraPos.x = pow.Lerp(transform.position.x, targetPosition.x);
				currentCameraPos.y = pow.Lerp(transform.position.y, targetPosition.y);
				currentCameraPos.z = transform.position.z;

				if (needShake)
				{
					shakeFrame--;
					var shakeAmplitude = Mathf.Lerp(0.2f, 0.0f, shakeFrame / 30);
					if (shakeFrame % 2 == 0)
					{
						transform.position = new Vector3(currentCameraPos.x + Random.Range(-shakeAmplitude, shakeAmplitude),
													 currentCameraPos.y + Random.Range(-shakeAmplitude, shakeAmplitude),
													 currentCameraPos.z);
					}
					
					if (shakeFrame <= 0)
					{
						needShake = false;
					}
				}
				else
				{
					transform.position = currentCameraPos;
					// transform.PositionX(
					// 	pow.Lerp(transform.position.x, targetPosition.x)
					// );
					// transform.PositionY(
					// 	pow.Lerp(transform.position.y, targetPosition.y)
					// );
				}
			}
		}
	}
}
