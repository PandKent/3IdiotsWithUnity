using System;
using System.Collections.Generic;
using Framework.Singleton;

namespace Framework.UIFramework
{
    public class UIMgr:TSingleton<UIMgr>
    {
        private Dictionary<string,UIWnd> m_wndCtrls = new Dictionary<string, UIWnd>();
        private Dictionary<UIWndName,UIWnd> m_bindDic = new Dictionary<UIWndName, UIWnd>();
        private Queue<UIWnd> m_waitForOpen = new Queue<UIWnd>();
        private Queue<object> m_wndPassData = new Queue<object>();

        public void Prepare()
        {
        }

        private void Update()
        {
            OpenProcess();
        }

        private void OnDestroy()
        {
            CloseAllWnd();
        }

        private void OpenProcess()
        {
            if (m_waitForOpen.Count == 0)
            {
                return;
            }

            UIWnd wnd = m_waitForOpen.Dequeue();
            object data = m_wndPassData.Dequeue();
            m_wndCtrls.Add(wnd.WndName(),wnd);
            wnd.OnBeforOpen(data);
            wnd.OnOpen(data);
        }

        public void UIBinder(UIWndName name, UIWnd ctrl)
        {
            if (!m_bindDic.ContainsKey(name))
                m_bindDic.Add(name, ctrl);
            else
                m_bindDic[name] = ctrl;
        }

        public void OpenWnd(UIWndName wnd, params object[] data)
        {
            if (!m_bindDic.ContainsKey(wnd))
                return;
            m_waitForOpen.Enqueue(m_bindDic[wnd]);
            m_wndPassData.Enqueue(data);
        }

        public void CloseAllWnd()
        {
            foreach (KeyValuePair<string,UIWnd> wnd in m_wndCtrls)
            {
                wnd.Value.OnClose();
            }
            m_wndCtrls.Clear();
        }

        public void CloseWndByName(string wndName, params object[] data)
        {
            if (m_wndCtrls.ContainsKey(wndName))
            {
                m_wndCtrls[wndName].OnClose(data);
                m_wndCtrls.Remove(wndName);
            }
        }
    }
}