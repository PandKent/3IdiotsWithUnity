using System;
using Framework.UIFramework;
using Singleton;
using UnityEngine;

namespace Game
{
    public class Main:MonoBehaviour
    {
        private void Awake()
        {
            UIMgr.Instance.Prepare();
            GameMgr.Instance.GameStart();
        }
    }
}