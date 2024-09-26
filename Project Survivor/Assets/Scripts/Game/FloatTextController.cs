using UnityEngine;
using QFramework;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace ProjectSurvivor
{
	public partial class FloatTextController : ViewController
	{

		public static FloatTextController Instance;

		private void Awake() {
			Instance = this;
		}

		private void OnDestroy() {
			Instance = null;
		}

		void Start()
		{
			FloatContainer.Hide();
		}

		public static void Play(Vector3 position, string text)
		{
			Instance.FloatContainer.InstantiateWithParent(Instance.transform)
			.Position(position.x, position.y)
			.Self(f => {
				var textTrans = f.transform.Find("Text");
				var txtEle = textTrans.GetComponent<Text>();
				txtEle.text = text;

				var oriPostionY = position.y;
				ActionKit.Lerp(0, 0.5f, 0.5f, p => {
					f.PositionY(oriPostionY + p);
					txtEle.color = Color.white.WithAlpha(1- (p * 2f));
				}, ()=>{
					textTrans.DestroyGameObjGracefully();
				}).Start(txtEle);
				//new Vector3(position.x, position.y, textTrans.position.z);
			}).Show();


		}
	}
}
