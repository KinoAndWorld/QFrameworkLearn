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
        public static BindableProperty<int> Level = new BindableProperty<int>();

        public static BindableProperty<float> CurrentTime = new BindableProperty<float>();


        public static void ResetData() {
            Exp.Value = 0;
            Level.Value = 1;
            CurrentTime.Value = 0.0f;
            Player.Instance.simpleAbility.damange.Value = 1.0f;
        }
    }
}
