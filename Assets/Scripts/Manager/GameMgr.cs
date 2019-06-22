using System;
using Framework.Singleton;
using Framework.UIFramework;
using Framework.UIFramework.Enum;

namespace Singleton
{
    public class GameMgr:TSingleton<GameMgr>
    {

        public void GameStart()
        {
            Init();
        }

        private void Init()
        {
            UIMgr.Instance.OpenWnd(UIWndName.MainUI);
        }

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}