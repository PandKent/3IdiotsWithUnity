
namespace Framework.UIFramework
{
    public interface UIWnd
    {
        string WndName();
        void OnBeforOpen(params object[] data);
        void OnOpen(params object[] data);
        void OnClose(params object[] data);
    }
}