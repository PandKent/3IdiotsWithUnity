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
//            UIMgr.Instance.OpenWnd(UIWndName.MainUI);
//            UIMgr.Instance.OpenWnd(UIWndName.MainUI1);
//            UIMgr.Instance.OpenWnd(UIWndName.MainUI1);
            UIMgr.Instance.OpenWnd(UIWndName.MainUI,"Hello World","Lao si NI hao","wo hao shuai");
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