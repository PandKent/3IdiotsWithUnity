using System.Collections.Generic;
using Framework.Singleton;

namespace Framework.UIFramework
{
    public class ResMgr:TSingleton<ResMgr>
    {
        private Dictionary<string,IRes> m_resDic = new Dictionary<string, IRes>();
        private List<IRes> m_resList = new List<IRes>();

        private bool m_isWorking = true;
        private bool m_isResDicDirty = false;
        
        //获取资源对象，如果是首次获取则创建资源对象
        public IRes GetRes(string name, bool isNew = false)
        {
            return null;
        }
        
        //通过IRes对象实例来获取对应的资源对象，相当于检索功能
        public R GetRes<R>(string name) where R : IRes
        {
            return default(R);
        }

        //通过UnityEngine.Object对象实例来获取对应的资源对象，相当于检索功能，主要正对资源集
        public R GetAsset<R>(string name) where R : UnityEngine.Object
        {
            return null;
        }

        private void Update()
        {
            
        }

        //释放那些未被使用或未被持有的脏资源
        private void RemoveUnusedRes()
        {
            
        }
    }
}