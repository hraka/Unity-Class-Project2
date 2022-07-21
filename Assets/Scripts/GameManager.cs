using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public GameObject ground;
    public Transform earth;
    public GameObject item;
    public Player player;
    public bool triger = true;
    public int mapPositiveIndex;
    public int mapNegativeIndex;
    public int itemPerGround;
    public Text guideMessage;
    public Bag bag;

    bool setMessage;

    public float messageTime;



   
    // Start is called before the first frame update
    void Start()
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
        instance0.GetComponent<SpriteRenderer>().color = Color.gray;
        instance0.name = "Ground";

    }

    // Update is called once per frame
    void Update()
    {
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
            var instance = Instantiate(ground, new Vector3((mapPositiveIndex + 1) * 10, -5, 0), Quaternion.identity, earth);
            instance.name = "Ground";
            mapPositiveIndex += 1;
            if (mapPositiveIndex % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;


            for (int i = 0; i < itemPerGround; i++)
            {
                var itemInstance = Instantiate(item, new Vector3(Random.Range(mapPositiveIndex * 10 - 5f, mapPositiveIndex * 10 + 5f), Random.Range(-3f, 0f), 0), Quaternion.identity);
                itemInstance.name = "Item";
            }
            
        }
        if (player.transform.position.x < (mapNegativeIndex) * -10)
        {
            var instance = Instantiate(ground, new Vector3((mapNegativeIndex + 1) * 10 * -1, -5, 0), Quaternion.identity, earth);
            instance.name = "Ground";
            mapNegativeIndex += 1;
            if (mapNegativeIndex % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;

            for (int i = 0; i < itemPerGround; i++)
            {
                var itemInstance = Instantiate(item, new Vector3(Random.Range(mapNegativeIndex * -10 - 5f, mapNegativeIndex * -10 + 5f), Random.Range(-3f, 0f), 0), Quaternion.identity);
                itemInstance.name = "Item";
            }
        }
    }

    

    public void SetMessage(string message)
    {
        setMessage = true;
        guideMessage.text = message;
    }

}
