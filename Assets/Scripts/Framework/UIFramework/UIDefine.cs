using System.Collections;
using System.Collections.Generic;
using Framework.Singleton;
using Framework.UIFramework;
using Framework.UIFramework.Enum;
using UnityEngine;

[System.Serializable] 
public class WndDefine
{
    public UIWndName wnd;
    public GameObject prefab;
    public WndExclusion exclusion;
    public WndLayer layer =  WndLayer.Window;
}

public class UIDefine:TSingleton<UIDefine>
{
    public WndDefine[] wnds;
    private Dictionary<UIWndName,WndDefine> defineDic = new Dictionary<UIWndName, WndDefine>();
    public void Awake()
    {
        if (wnds!=null)
        {
            for (int i = 0; i < wnds.Length; i++)
            {
                if (defineDic.ContainsKey(wnds[i].wnd))
                    continue;
                defineDic.Add(wnds[i].wnd,wnds[i]);
            }
        }
    }

    public GameObject GetWndPrefab(UIWndName wnd)
    {
        if (defineDic.ContainsKey(wnd))
            return defineDic[wnd].prefab;
        else
            return null;
    }
        
    public WndDefine GetWndDefine(UIWndName wnd)
    {
        if (defineDic.ContainsKey(wnd))
            return defineDic[wnd];
        else
            return null;
    }
}
