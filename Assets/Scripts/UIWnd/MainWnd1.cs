using Framework.UIFramework;
using Framework.UIFramework.Enum;
using UnityEngine;

namespace UIWndScripts
{
    public class MainWnd1:UIWnd
    {
        public override UIWndName WndName()
        {
            return UIWndName.MainUI1;
        }

        public override void OnBeforOpen(params object[] data)
        {
            
        }

        public override void OnOpen(params object[] data)
        {
            Debug.Log("Main UI 1 Opened");
        }

        public override void OnClose(params object[] data)
        {
            Debug.Log("Main UI 1 Closed");
        }
    }
}