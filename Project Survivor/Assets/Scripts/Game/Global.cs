using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using MoonSharp.VsCodeDebugger.SDK;

namespace ProjectSurvivor
{
    public class Global : MonoBehaviour
    {
        public static BindableProperty<int> Exp = new BindableProperty<int>();
        public static BindableProperty<int> Coin = new BindableProperty<int>();
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentTime = new BindableProperty<float>();


        public static void ResetData() {
            Exp.Value = 0;
            Coin.Value = 0;
            Level.Value = 1;
            CurrentTime.Value = 0.0f;
            Player.Instance.simpleAbility.damange.Value = 1.0f;
            Player.Instance.simpleAbility.frequent.Value = 1.5f;
            EnemyGenerator.EmemyCount.Value = 0;
        }

        public static int ToNextLvExp() {
            return Global.Level.Value * 5;
        }

        public static void GeneratePowerUp(GameObject target) {
            // var exp = Instantiate<GameObject>();
            // 90% exp
            // 
            var random = Random.Range(0, 100.0f);
            if (random <= 90.0f)
            {
                PowerUpManager.Instance.Exp.Instantiate()
                .Position(target.Position())
                .Show();
            } else {
                "掉落金币".LogInfo();
                PowerUpManager.Instance.Coin.Instantiate()
                .Position(target.Position())
                .Show();
            }
        }
    }
}
