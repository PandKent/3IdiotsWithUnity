using System.Collections.Generic;
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
            Debug.Log(((object[])data[0])[0].ToString());
            Debug.Log(((object[])data[0])[1].ToString());
            Debug.Log(((object[])data[0])[2].ToString());
        }

        public override void OnClose(params object[] data)
        {
            Debug.Log("Main UI Closed");
            UIMgr.Instance.OpenWnd(UIWndName.MainUI1,"AAA","BBB");
        }
    }
}