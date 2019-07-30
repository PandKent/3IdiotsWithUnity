using System.Collections.Generic;
using Framework.Singleton;

namespace Framework.UIFramework
{
    public class ResMgr:TSingleton<ResMgr>
    {
        private Dictionary<string,IRes> m_resDic = new Dictionary<string, IRes>();
        private List<IRes> m_resList = new List<IRes>();
        
        
        public void GetRes(string name)
        {
            
        }
    }
}