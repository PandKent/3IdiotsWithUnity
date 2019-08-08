using System;
using System.Collections.Generic;

namespace Framework.UIFramework
{
    public class ResLoader
    {
        private List<IRes> m_resList = new List<IRes>();
        private LinkedList<IRes> m_waitLoadLinkList = new LinkedList<IRes>(); //使用链表结构来管理等待加载的资源，链表能大幅度提高内存使用率和加载速度
        private Action m_listener;

        private bool m_cacheFlag = false;
        private int m_loadingCount = 0;
        private string m_loaderName;
        
        private static List<ResLoader> m_activeLoaderList = new List<ResLoader>();
        

        public static ResLoader Allocate(string name = null) //之后可添加策略模式
        {
            //TODO 从对象池内获取对象

            return null;
        }

        public bool Add2Load(string name, Action<bool, IRes> listener = null, bool lastOrder = true)
        {
            return true;
        }

        public void LoadSync()
        {
        }

        public void LoadAsync(Action listener = null)
        {
        }
    }
}