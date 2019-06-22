using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //step = GameObject.Find("YellowSh").GetComponent<ShMove>().step;
        //y = Random.Range(-5, 5);
        //x = Random.Range(-6, 11);
        do
        {
            tempy = Random.Range(-5, 5);
            tempx = Random.Range(-6, 11);
        }
        while ((Mathf.Abs(tempx - x) <= 1) | (Mathf.Abs(tempy - y) <= 1));

        
        x = tempx;
        y = tempy;

        //gameObject.transform.localPosition = new Vector3(x * step, y * step, 0);
        Food.transform.localPosition = new Vector3(x * step, y * step, 0);


    }
}
