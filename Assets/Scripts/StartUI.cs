using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour
{
    public Text lastscore;
    public Text bestscore;
    public Toggle blue;
    public Toggle yellow;


    void Awake()
    {
        lastscore.text = "上次得分：" + PlayerPrefs.GetInt("lastscore",0);
        bestscore.text = "最好得分：" + PlayerPrefs.GetInt("bestscore", 0);
    }
    void Start()
    {
        if (PlayerPrefs.GetString("snake") == "blue")
        {
            blue.isOn = true;
        }
        else
        {
            yellow.isOn = true;
        }
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        
    }
    public void bluetoggle(bool isOn)
    {
        PlayerPrefs.SetString("snake", "blue");
        Debug.Log(PlayerPrefs.GetString("snake"));
    }
    public void yellowtoggle(bool isOn)
    {
        PlayerPrefs.SetString("snake", "yellow");
        Debug.Log(PlayerPrefs.GetString("snake"));
    }
}
