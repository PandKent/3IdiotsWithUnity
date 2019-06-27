using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    // Start is called before the first frame update
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }

    int x, y;
    int tempx, tempy;
    float step ;

    public GameObject foodPrefab;
    private Transform foodHolder;
    public Sprite[] foodSprites=new Sprite[10];
    // public GameObject temp;
    
    void Awake()
    {
        _instance = this;
        x = 0;
        y = 0;
    }

    void Start()

    {
        // Debug.Log("Start");
        step = GameObject.Find("YellowSh").GetComponent<ShMove>().step;
        
        foodHolder = GameObject.Find("FoodHolder").transform;
        Debug.Log(step);
        MakeFood();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void MakeFood()
    {

        GameObject Food = Instantiate(foodPrefab);
        Food.transform.SetParent(foodHolder,false);
        Food.GetComponent<Image>().sprite = foodSprites[Random.Range(0, 10)];
        foodHolder.SetAsLastSibling();


        //step = GameObject.Find("YellowSh").GetComponent<ShMove>().step;
        do
        {
            tempy = Random.Range(-5, 5);
            tempx = Random.Range(-6, 11);
        }
        while ((x == tempx) & (y == tempy));
     




        x = tempx;
        y = tempy;

        //gameObject.transform.localPosition = new Vector3(x * step, y * step, 0);
        Food.transform.localPosition = new Vector3(x * step, y * step, 0);


    }
}
