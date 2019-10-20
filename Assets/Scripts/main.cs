using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    [SerializeField]
    public string path;
    [SerializeField]
    public TextAsset txt;

    public KakaoAnalysize kakao = new KakaoAnalysize();
    // Start is called before the first frame update
    void Start()
    {
//        if(!string.IsNullOrEmpty(path))
            kakao.InitSystem(path,txt);
    }

    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;    //设置背景填充
        fontStyle.normal.textColor= new Color(1,1,1);   //设置字体颜色
        fontStyle.fontSize = 40;
        
        int i = 0;
        foreach (var value in kakao.displayData)
        {
            
            GUI.Label( new Rect( Screen.width * 0.5f , Screen.height * 0.5f + (i*100), 500, 300) , $"{value.content} : {value.amount}",fontStyle);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
