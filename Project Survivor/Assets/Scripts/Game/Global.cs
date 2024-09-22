using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using MoonSharp.VsCodeDebugger.SDK;

namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        public static BindableProperty<int> Exp = new BindableProperty<int>();
        public static BindableProperty<int> Coin = new BindableProperty<int>();
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentTime = new BindableProperty<float>();

        public static BindableProperty<float> ExpDropPrec = new BindableProperty<float>(0.4f);
        public static BindableProperty<float> CoinDropPrec = new BindableProperty<float>(0.1f);


        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit() {
            Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);
            // 金币
            Global.Coin.Register(coin => {
                PlayerPrefs.SetInt(nameof(coin), coin);
            });

            Global.ExpDropPrec.Value = PlayerPrefs.GetFloat(nameof(ExpDropPrec), 0.4f);
            // 经验掉率
            Global.ExpDropPrec.Register(exp => {
                PlayerPrefs.SetFloat(nameof(ExpDropPrec), exp);
            });
            Global.CoinDropPrec.Value = PlayerPrefs.GetFloat(nameof(CoinDropPrec), 0.1f);
            // 金币掉率
            Global.CoinDropPrec.Register(coin => {
                PlayerPrefs.SetFloat(nameof(CoinDropPrec), coin);
            });


        }

        public static void ResetData() {
            Exp.Value = 0;
            Level.Value = 1;
            CurrentTime.Value = 0.0f;
            if (Player.Instance) {
                Player.Instance.simpleAbility.damange.Value = 1.0f;
                Player.Instance.simpleAbility.frequent.Value = 1.5f;
            }
            EnemyGenerator.EmemyCount.Value = 0;
        }

        public static int ToNextLvExp() {
            return Global.Level.Value * 5;
        }

        public static void GeneratePowerUp(GameObject target) {
            // var exp = Instantiate<GameObject>();
            // 90% exp
            // 
            var random = Random.Range(0, 1f);

            if (random < ExpDropPrec.Value)
            {
                PowerUpManager.Instance.Exp.Instantiate()
                .Position(target.Position())
                .Show();
            }

            random = Random.Range(0, 1f);
            if (random < CoinDropPrec.Value) {
                "掉落金币".LogInfo();
                PowerUpManager.Instance.Coin.Instantiate()
                .Position(target.Position())
                .Show();
            }
        }

        protected override void Init() {

        }
    }
}
