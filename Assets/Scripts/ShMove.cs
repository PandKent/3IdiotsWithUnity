using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShMove : MonoBehaviour
{
    // Start is called before the first frame update
    public int step;
    float x;
    float y;
    
    Vector3 shPos,bodyPos;
    //Vector3 tempVector;
    public GameObject snakebodyPrefabsBlue;
    public GameObject snakebodyPrefabsYellow;
    //public GameObject dieLogPre;
    public List<Transform> bodylist = new List<Transform>();
    public List<Vector3> headList = new List<Vector3>();  
    int len;//body长度
    int speed;//速度
    Transform canvas;
    GameObject body;
    public GameObject dieLog;
    public Sprite BlueHead;
    public Sprite YellowHead;
    //public GameObject Head;


    //GameObject[]=SnakeBody[];



    //public int _step
    //{
    //    get;set;
    //}
    void Awake()
    {
        step = 30;
        canvas = GameObject.Find("Canvas").transform;
        //Head = GameObject.Find("YellowSh")
        x = 0;
        y = step;
        len = 0;
        speed = 8;
        bodylist.Clear();
        headList.Clear();



    }
    void Start()
    {
        //step = 30;
        //velocity = 0.35f;
        
        if (PlayerPrefs.GetString("snake") == "blue")
        {
            gameObject.GetComponent<Image>().sprite = BlueHead;
           // Debug.Log("blue");
        }
        if (PlayerPrefs.GetString("snake") == "yellow")
        {
            gameObject.GetComponent<Image>().sprite = YellowHead;
            //Debug.Log("yellow");
        }

        dieLog = GameObject.Find("DieLog");
        dieLog.SetActive(false);

       // InvokeRepeating("Move", 0, velocity);
        //print(shPos);
    







    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            x = 0; y = step;
            
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            x = 0; y = -step;
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -step; y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = step; y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        Move();
    }


    void Move()
    {
        shPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = Vector3.Lerp(shPos, shPos + new Vector3(x, y, 0), speed  * Time.deltaTime);
        bdMove();
    }
    public void bdMove()
    {

        if (len > 0)
        {
            
            bodylist[0].localPosition = Vector3.Lerp(bodylist[0].localPosition, shPos, speed * Time.deltaTime);
            if (len > 1)
            {

                for (int i = 0; i < len - 1; i++)
                {

                    bodylist[i + 1].localPosition = Vector3.Lerp(bodylist[i + 1].localPosition, bodylist[i].localPosition, speed * Time.deltaTime);

                }

            }
        }
        
            



      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Debug.Log(collision.gameObject.transform.localPosition);
            Destroy(collision.gameObject);
            FoodMaker.Instance.MakeFood();
            Grow();
            
            MainUI.Instance.ChangeUI(2);
            
            
        }

        if (collision.tag == "body" || collision.tag == "border")
        //if (collision.tag == "border")
        {
            Die();
        }


    }
    void Grow()
    {
        if(PlayerPrefs.GetString("snake")=="blue")
        {
            body = Instantiate(snakebodyPrefabsBlue);
        }
        else
        {
            body = Instantiate(snakebodyPrefabsYellow);
        }
        
        body.transform.SetParent(canvas,false);
        bodylist.Add(body.transform);
        
        len = bodylist.Count;
        if (len==1)
        {
           
            bodylist[0].localPosition = shPos;


        }
        if (len < 3)
        {
            body.tag = "Untagged";
        }

        //headList.Add(shPos);
        if (len>1)
        {

            bodylist[len - 1].localPosition = bodylist[len - 2].localPosition;

        }



       
    }
    void Die()
    {

        Time.timeScale = 0;
        dieLog.SetActive(true);
        dieLog.transform.SetAsLastSibling();
        if (MainUI.Instance.tempscore > PlayerPrefs.GetInt("bestscore",0))
        {
            PlayerPrefs.SetInt("bestscore", MainUI.Instance.tempscore);
        }

        PlayerPrefs.SetInt("lastscore", MainUI.Instance.tempscore);


    }


}