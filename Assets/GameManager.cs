using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public GameObject ground;
    public Transform earth;
    public GameObject item;
    public GameObject player;
    public bool triger = true;
    public int mapPositiveIndex;
    public int mapNegativeIndex;
    // Start is called before the first frame update
    void Start()
    {
        manager = this;


        for(int i = 0; i < 0; i++)
        {
            var instance = Instantiate(ground, new Vector3((i + 1) * ground.transform.localScale.x, -5, 0), Quaternion.identity, earth);
            if (i % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;
        }

        for (int i = 0; i < 5; i++)
        {
            var instance = Instantiate(item, new Vector3(Random.Range(0f, 30f), Random.Range(-3f, 0f), 0), Quaternion.identity);
            if (i % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.gray;
            instance.name = "Item";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > mapPositiveIndex * 5)
        { 
            var instance = Instantiate(ground, new Vector3((mapPositiveIndex + 1) * ground.transform.localScale.x, -5, 0), Quaternion.identity, earth);
            mapPositiveIndex += 1;
            if (mapPositiveIndex % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (player.transform.position.x < mapNegativeIndex * -5)
        {
            var instance = Instantiate(ground, new Vector3((mapNegativeIndex + 1) * ground.transform.localScale.x * -1, -5, 0), Quaternion.identity, earth);
            mapNegativeIndex += 1;
            if (mapNegativeIndex % 2 == 0)
                instance.GetComponent<SpriteRenderer>().color = Color.green;
        }

    }
}
