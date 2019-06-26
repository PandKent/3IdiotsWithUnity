using System;
using System.Collections.Generic;
using Framework.Singleton;
using Framework.UIFramework.Enum;
using UnityEngine;

namespace Framework.UIFramework
{
    public class UIMgr:TSingleton<UIMgr>
    {
        private Dictionary<UIWndName,UIWnd> m_wndCtrls = new Dictionary<UIWndName, UIWnd>();
//        private Dictionary<UIWndName,UIWnd> m_bindDic = new Dictionary<UIWndName, UIWnd>();
        private Queue<UIWnd> m_waitForOpen = new Queue<UIWnd>();
        private Queue<UIWnd> m_waitForClose = new Queue<UIWnd>();
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
            
            if(m_waitForClose.Count>0)
                m_waitForClose.Dequeue().OnClose();
        }

        private void CreateProcess(UIWndName wnd)
        {
            if(UIDefine.Instance.GetWndDefine(wnd)==null)
                return;
            WndDefine define = UIDefine.Instance.GetWndDefine(wnd);
            foreach (UIWndName name in m_wndCtrls.Keys)
            {
                WndDefine openingWnd = UIDefine.Instance.GetWndDefine(name);
                if (openingWnd != null)
                {
                    //如果冲突则将当前打开的窗口推入待关闭列表
                    if (define.exclusion == openingWnd.exclusion)
                    {
                        m_waitForClose.Enqueue(UIDefine.Instance.GetWndPrefab(openingWnd.wnd).GetComponent<UIWnd>());
                    }

                    if (define.prefab != null)
                    {
                        GameObject go = Instantiate(define.prefab);
                        
                    }
                    
                }
            }
        }

//        public void UIBinder(UIWndName name, UIWnd ctrl)
//        {
//            if (!m_bindDic.ContainsKey(name))
//                m_bindDic.Add(name, ctrl);
//            else
//                m_bindDic[name] = ctrl;
//        }

        public void OpenWnd(UIWndName wnd, params object[] data)
        {
            if(UIDefine.Instance.GetWndDefine(wnd)==null)
                return;
            m_waitForOpen.Enqueue(UIDefine.Instance.GetWndPrefab(wnd).GetComponent<UIWnd>());
            m_wndPassData.Enqueue(data);
        }

        public void CloseAllWnd()
        {
            foreach (KeyValuePair<UIWndName,UIWnd> wnd in m_wndCtrls)
            {
                wnd.Value.OnClose();
            }
            m_wndCtrls.Clear();
        }

        public void CloseWndByName(UIWndName wndName, params object[] data)
        {
            if (m_wndCtrls.ContainsKey(wndName))
            {
                m_wndCtrls[wndName].OnClose(data);
                m_wndCtrls.Remove(wndName);
            }
        }
    }
}