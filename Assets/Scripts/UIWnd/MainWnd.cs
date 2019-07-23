using Framework.UIFramework;
using Framework.UIFramework.Enum;
using UnityEngine;

namespace UIWndScripts
{
    public class MainWnd:UIWnd
    {
        public override UIWndName WndName()
        {
            return UIWndName.MainUI;
        }

        public override void OnBeforOpen(params object[] data)
        {
            
        }

        public override void OnOpen(params object[] data)
        {
            Debug.Log("Main UI Opened");
        }

        public override void OnClose(params object[] data)
        {
            Debug.Log("Main UI Closed");
        }
    }
}