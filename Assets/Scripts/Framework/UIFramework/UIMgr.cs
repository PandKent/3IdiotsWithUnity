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
        private GameObject m_root;
        private Dictionary<string,GameObject> m_layersDic = new Dictionary<string, GameObject>();
        
        public void Prepare()
        {
            CheckUIRoot();
        }

        private void CheckUIRoot()
        {
            m_root = GameObject.Find("Canvas");
            if (m_root == null)
            {
                Debug.LogError("Can Not Found UI Root/Canvas");
                //TODO 缺少资源加载流程
                return;
            }
            DontDestroyOnLoad(m_root);
            var wndRoot = m_root.gameObject.GetComponentsInChildren<Transform>();
            if (wndRoot.Length <= 1)
            {
                //TODO 缺少资源加载流程
                return;
            }

            var layers = wndRoot[1].GetComponentsInChildren<Transform>();
            if (layers.Length <=1)
            {
                foreach (var layerName in System.Enum.GetNames(typeof(WndLayer)))
                {
                    var go = new GameObject(layerName);
                    go.AddComponent<RectTransform>();
                    go.transform.SetParent(wndRoot[1]);
                    
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localScale = Vector3.one;
                    
                    var recT = go.GetComponent<RectTransform>();
                    recT.anchorMax = Vector3.one;
                    recT.anchorMin = Vector3.zero;
                    recT.offsetMin = new Vector2(0,0);
                    recT.offsetMax = new Vector2(0,0);
//                    go.GetComponent<RectTransform>().localScale = Vector3.one;
//                    go.GetComponent<RectTransform>().sizeDelta = new Vector2 (0, 0);

//                    go.GetComponent<RectTransform>().position = Vector3.zero;
                    
                    

                    if(!m_layersDic.ContainsKey(go.name))
                        m_layersDic.Add(go.name, go.gameObject);
                }

               
            }
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
//            wnd.gameObject.SetActive(true);//数据加载成功后，显示界面 目前均为同步加载模式，后期改异步
            wnd.OnOpen(data);

            if (m_waitForClose.Count > 0)
            {
                var go = m_waitForClose.Dequeue();
                go.OnClose();
                Destroy(go.gameObject);
            }
        }

        private void CreateProcess(UIWndName wnd)
        {
            if(UIDefine.Instance.GetWndDefine(wnd)==null)
                return;
            WndDefine opening = UIDefine.Instance.GetWndDefine(wnd);

            if(m_wndCtrls.Count <= 0)
            {
                if (opening.prefab != null)
                {
                    GameObject go = Instantiate(opening.prefab);
//                    go.SetActive(false); //初始化为不可见，这一步是为了给新界面OnBeforeOpen函数预留加载时间
                    if (m_layersDic.ContainsKey(opening.layer.ToString()))
                    {
                        //将新UI界面设置给对应的层级
                        go.transform.SetParent(m_layersDic[opening.layer.ToString()].transform);
                        go.transform.localScale = Vector3.one;
                        go.transform.position = Vector3.zero;
                        var recT = go.GetComponent<RectTransform>();
                        recT.anchorMax = Vector3.one;
                        recT.anchorMin = Vector3.zero;
                        recT.offsetMin = new Vector2(0,0);
                        recT.offsetMax = new Vector2(0,0);
                    }
                    else
                    {
                        Debug.LogError(string.Format("Can Not Found This UI's Layer  [UI][{0}]",opening.wnd.ToString()));
                    }
                }
                return;
            }
            
            foreach (UIWndName name in m_wndCtrls.Keys)
            {
                WndDefine opened = UIDefine.Instance.GetWndDefine(name);
                if (opened != null)
                {
                    //如果冲突则将当前打开的窗口推入待关闭列表
                    if (opening.exclusion == opened.exclusion)
                    {
                        m_waitForClose.Enqueue(UIDefine.Instance.GetWndPrefab(opened.wnd).GetComponent<UIWnd>());
                    }

                    if (opening.prefab != null)
                    {
                        GameObject go = Instantiate(opening.prefab);
//                        go.SetActive(false); //初始化为不可见，这一步是为了给新界面OnBeforeOpen函数预留加载时间
                        if (m_layersDic.ContainsKey(opening.layer.ToString()))
                        {
                            //将新UI界面设置给对应的层级
                            go.transform.SetParent(m_layersDic[opening.layer.ToString()].transform);
                            go.transform.localScale = Vector3.one;
                            go.transform.position = Vector3.zero;
                            var recT = go.GetComponent<RectTransform>();
                            recT.anchorMax = Vector3.one;
                            recT.anchorMin = Vector3.zero;
                            recT.offsetMin = new Vector2(0,0);
                            recT.offsetMax = new Vector2(0,0);
                        }
                        else
                        {
                            Debug.LogError(string.Format("Can Not Found This UI's Layer  [UI][{0}]",opening.wnd.ToString()));
                        }
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
            CreateProcess(wnd);
            m_waitForOpen.Enqueue(UIDefine.Instance.GetWndPrefab(wnd).GetComponent<UIWnd>());
            m_wndPassData.Enqueue(data);
        }

        public void CloseAllWnd()
        {
            foreach (KeyValuePair<UIWndName,UIWnd> wnd in m_wndCtrls)
            {
                wnd.Value.OnClose();
            }
            foreach (KeyValuePair<UIWndName,UIWnd> wnd in m_wndCtrls)
            {
                Destroy(wnd.Value.gameObject);
            }
            m_wndCtrls.Clear();
            m_waitForClose.Clear();
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