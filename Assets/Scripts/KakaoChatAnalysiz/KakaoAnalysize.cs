using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakaoAnalysize
{
    public string filePath;
    public TextAsset _textAsset;
    public string myTxt;
    public KakaoData data = new KakaoData();
    public List<DisplayData> displayData = new List<DisplayData>();
    
    public void InitSystem(string path, TextAsset txt)
    {
        _textAsset = txt;
        myTxt = _textAsset.text;
        Debug.Log(myTxt);
        
        ReadData(myTxt);
    }
    
    public void ReadData(string content)
    {
        string[] lines = content.Split('\n');
        int totalMsg = 0;
        foreach (string line in lines)
        {
            ParseData(line);
        }
        Debug.Log("---------------------------[Final]-----------------------------");
        Debug.Log("[活跃用户数量] "+data.contentByUser.Count);
        foreach (var value in data.contentByUser)
        {
//            Debug.Log($"[{value.Key}] : [{value.Value.Count}]");
            totalMsg += value.Value.Count;
        }
        Debug.Log($"[今日总发言数] : [{totalMsg}]");
//        Debug.Log($"[Nora.T] : [1314]");
        foreach (var value in data.contentByUser)
        {
            Debug.Log($"[{value.Key}] : [{value.Value.Count}]");
//            totalMsg += value.Value.Count;
        }
        DisplayData temp = new DisplayData();
        temp.content = "今日总发言数";
        temp.amount = totalMsg.ToString();
        displayData.Add(temp);
        temp = new DisplayData();
        temp.content = "活跃用户数量";
        temp.amount = data.contentByUser.Count.ToString();
        displayData.Add(temp);
    }

    private void ParseData(string line)
    {
        string start = line.Substring(0, 5);
        if(start!="2019年")
            return;
//        Debug.Log("[Content]"+line);
        int firstBlankIndex = line.IndexOf(" ");
        string date = line.Substring(0, firstBlankIndex);//日期
//        Debug.Log("[Date]"+date);
        int firstCommaIndex = line.IndexOf(",");
        if(firstCommaIndex < 0)
            return;
//        Debug.Log(firstCommaIndex);
        string time = line.Substring(firstBlankIndex+1, firstCommaIndex-firstBlankIndex-1);
//        Debug.Log("[Time]" + time);
        int userNameIndex = line.IndexOf(":", firstCommaIndex + 1);
        string user = line.Substring(firstCommaIndex + 1, userNameIndex - firstCommaIndex - 2);
//        Debug.Log("[User]" + user);
        string send = line.Substring(userNameIndex + 2);
//        Debug.Log("[send]" + send);
        
        personalData personalData = new personalData();
        personalData.content = send;
        
        if (data.contentByUser.ContainsKey(user))
        {
            data.contentByUser[user].Add(personalData);
        }
        else
        {
            data.contentByUser.Add(user,new List<personalData>());
            data.contentByUser[user].Add(personalData);
        }
    }
}

public class KakaoData
{
    public Dictionary<string, List<personalData>> contentByDate = new Dictionary<string, List<personalData>>();
    public Dictionary<string, List<personalData>> contentByUser = new Dictionary<string, List<personalData>>();
}

public class personalData
{
    public string userName;
    public string date;
    public string time;
    public string content;

}

public class DisplayData
{
    public string content;
    public string amount;
}
