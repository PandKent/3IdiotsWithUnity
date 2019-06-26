
using Framework.UIFramework.Enum;
using UnityEngine;

namespace Framework.UIFramework
{
    public abstract class UIWnd:MonoBehaviour
    {
        public abstract UIWndName WndName();
        public abstract void OnBeforOpen(params object[] data);
        public abstract void OnOpen(params object[] data);
        public abstract void OnClose(params object[] data);
    }
}