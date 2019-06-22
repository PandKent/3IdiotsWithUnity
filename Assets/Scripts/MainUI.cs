using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{ 
    public Text score;
    public int tempscore = 0;
    public Sprite pause;
    public Sprite play;
    bool isPause = false;
    private static MainUI _instance;
    public static MainUI Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        score.text = "0";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeUI(int i=1)
    {
        tempscore += i;
        score.text =tempscore.ToString();

    }
    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        
    }
    public void Pause()
    {
        isPause = !isPause;
        if (isPause == true)
        {
            Time.timeScale = 0;
            GameObject.Find("Pause").GetComponent<Image>().sprite=play;

        }
        else
        {
            Time.timeScale = 1;
            GameObject.Find("Pause").GetComponent<Image>().sprite = pause;
        }

    }
}
