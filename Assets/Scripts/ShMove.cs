using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float step;
    public float velocity;//repeting时间间隔，越小速度越快
    float x;
    float y;
    float delTime;
    
    Vector3 shPos,bodyPos;
    //Vector3 tempVector;
    public GameObject snakebodyPrefabsBlue;
    public GameObject snakebodyPrefabsYellow;
    //public GameObject dieLogPre;
    public List<Transform> bodylist = new List<Transform>();
    public List<Vector3> headList = new List<Vector3>();  
    int len;//body长度
    int movecount;//记录帧数
    int smooth;//速度，可取2或5，保证movebuf为整数
    int movebuf;//蛇头移动帧数缓存
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
        velocity = 0.1f;
        canvas = GameObject.Find("Canvas").transform;
        //Head = GameObject.Find("YellowSh")
        x = 0;
        y = step;
        len = 0;
        movecount = 0;
        smooth = 5;
        delTime = 0.02f;
        //movebuf = 25;
        movebuf = (int)(1 / (smooth * delTime));
        bodylist.Clear();
        headList.Clear();
        Debug.Log(movecount);


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

        //if (Time.frameCount % 5 == 0)
        //{
        //    Move();
        //}
        //Move();
    }


    void Move()
    {
        bdMove();
        shPos = gameObject.transform.localPosition;

        //gameObject.transform.localPosition = new Vector3(shPos.x + x, shPos.y + y, shPos.z);
        //卡顿移动 
        //gameObject.transform.localPosition = shPos + new Vector3(x, y, 0);
        //平滑移动vector2.lerp;
        gameObject.transform.localPosition = Vector3.Lerp(shPos, shPos + new Vector3(x, y, 0), smooth  * Time.deltaTime);
        
        headList.Add(gameObject.transform.localPosition);
        if (movecount < movebuf * (len + 1))
        {
            movecount++;

            
        }
        else
        {
            headList.RemoveAt(0);
            
                
            
        }
 
    }
    public void bdMove()
    {

        //蛇尾移动到蛇头
        //bodylist[len - 1].localPosition = shPos;
        //bodylist.Insert(0, bodylist[len - 1]);
        //bodylist.RemoveAt(len);
        //逐个结点移动

        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {

                bodylist[i].localPosition = headList[movecount - (i+1) * movebuf];

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
            body.tag="Untagged";
            
        }




        //Debug.Log("Grow"+"长度："+len);
        //Debug.Log(body.transform.localPosition);
    }
    void Die()
    {
        //CancelInvoke();
        //dieLog = Instantiate(dieLogPre);
        //dieLog.transform.SetParent(canvas, false);

        //dieLog.GetComponent<Button>().onClick.AddListener(Home);
        Time.timeScale = 0;
        dieLog.SetActive(true);
        if (MainUI.Instance.tempscore > PlayerPrefs.GetInt("bestscore",0))
        {
            PlayerPrefs.SetInt("bestscore", MainUI.Instance.tempscore);
        }

        PlayerPrefs.SetInt("lastscore", MainUI.Instance.tempscore);

        //bodylist.Clear();
        //headList.Clear();
    }
    //void Home()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    //}

}