using Framework.Singleton;
using UIWndScripts;

namespace Framework.UIFramework
{
    public class UIRegister:TSingleton<UIRegister>
    {
        public void RegisterUI()
        {
            UIMgr.Instance.UIBinder(UIWndName.MainUI, new MainWnd());
        }
    }

    public enum UIWndName
    {
        MainUI,
    }
}