using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public GameObject ground;
    public Transform earth;
    public GameObject[] items;
    public Player player;
    public bool triger = true;
    public int mapPositiveIndex;
    public int mapNegativeIndex;
    public int itemPerGround;
    public Text guideMessage;
    public Bag bag;

    bool setMessage;

    public float messageTime;

    bool m_bPause = false;

    float m_runCount = 0;

    public int maxHeight;
    public int targetHeight;
    public GameObject star;



   
    // Start is called before the first frame update
    void Awake()
    {
        manager = this;

        guideMessage.text = "";
        // itemImageDictionary.Add(1, Resources.Load<Image>("Item1"));

        /*for (int i = 0; i < 0; i++)
        {
            var instance = Instantiate(ground, new Vector3((i + 1) * ground.transform.localScale.x, -5, 0), Quaternion.identity, earth);
            instance.name = "Ground";
            
            if (i % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;
        }*/
        
        var instance0 = Instantiate(ground, new Vector3(0, -5, 0), Quaternion.identity, earth);
        //instance0.GetComponent<SpriteRenderer>().color = Color.gray;
        instance0.name = "Ground";
        CreateItems(0 * 10);
        for (int i = 0; i < 2; i++)
        {
            var instance1 = Instantiate(ground, new Vector3((i + 1) * 10, -5, 0), Quaternion.identity, earth);
            //instance1.GetComponent<SpriteRenderer>().color = Color.gray;
            instance1.name = "Ground";
            CreateItems((i + 1) * 10);
            var instance2 = Instantiate(ground, new Vector3((i + 1) * -10, -5, 0), Quaternion.identity, earth);
            //instance2.GetComponent<SpriteRenderer>().color = Color.gray;
            instance2.name = "Ground";
            CreateItems((i + 1) * -10);
        }

        SetMessage("F를 눌러 상호작용 하세요");


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!m_bPause)
        {
            m_runCount += Time.deltaTime;
        }

        InstantiateMap();


        if(setMessage)
        {

            messageTime += Time.deltaTime;
            if(messageTime > 1.5)
            {
                setMessage = false;
                guideMessage.text = "";
                messageTime = 0;
            }
        }

    }

    public void InstantiateMap()
    {

        if (player.transform.position.x > mapPositiveIndex * 10)
        {
            var instancePosIndex = mapPositiveIndex + 3;
            var instance = Instantiate(ground, new Vector3(instancePosIndex * 10, -5, 0), Quaternion.identity, earth);
            instance.name = "Ground";
            mapPositiveIndex += 1;
            
            if (mapPositiveIndex % 2 == 0) { }
            //instance.GetComponent<SpriteRenderer>().color = Color.green;


            CreateItems(instancePosIndex * 10);

            /*for (int i = 0; i < itemPerGround; i++)
            {
                var item = items[Random.Range(0, items.Length)];
                var itemInstance = Instantiate(item, new Vector3(Random.Range(instancePosIndex * 10 - 5f, instancePosIndex * 10 + 5f), Random.Range(-3f, 0f), 0), Quaternion.identity);
                itemInstance.name = "Item";
            }*/

        }
        if (player.transform.position.x < mapNegativeIndex * -10)
        {
            var instancePosIndex = mapNegativeIndex + 3;
            var instance = Instantiate(ground, new Vector3(instancePosIndex * 10 * -1, -5, 0), Quaternion.identity, earth);
            instance.name = "Ground";
            mapNegativeIndex += 1;

            if (mapNegativeIndex % 2 == 0) { }
            //instance.GetComponent<SpriteRenderer>().color = Color.green;


            CreateItems(instancePosIndex * -10);
            /*for (int i = 0; i < itemPerGround; i++)
            {
                var item = items[Random.Range(0, items.Length)];
                var itemInstance = Instantiate(item, new Vector3(Random.Range(instancePosIndex * -10 - 5f, instancePosIndex * -10 + 5f), Random.Range(-3f, 0f), 0), Quaternion.identity);
                itemInstance.name = "Item";
            }*/
        }
    }

    public void CreateItems(int mapPos)
    {
        for (int i = 0; i < itemPerGround; i++)
        {
            var item = items[Random.Range(0, items.Length)];
            var itemInstance = Instantiate(item, new Vector3(Random.Range(mapPos - 5f, mapPos + 5f), Random.Range(-3f, 0f), 0), Quaternion.identity);
            itemInstance.name = "Item";
        }
    }
    

    public void SetMessage(string message)
    {
        setMessage = true;
        guideMessage.text = message;
    }

    public void SetPause()
    {
        OnApplicationPause(!m_bPause);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        m_bPause = pauseStatus;
    }

    public void UpperStar()
    {
        if(maxHeight > targetHeight)
        {
            SetMessage("별까지 쌓았다...!");

            targetHeight *= 2;

            if (targetHeight > maxHeight)
                targetHeight = maxHeight;

            player.SetTarget();
        } else
        {
            SetMessage("별에 닿았다!");
        }
    }

}
