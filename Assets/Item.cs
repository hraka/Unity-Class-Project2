using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public bool isCount;
    public bool isFrontObject;
    bool isPickedUp;
    public int itemCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCount && Input.GetKeyDown(KeyCode.Space))
        {
            PickedUp();
        }


        if (isPickedUp && Input.GetKeyDown(KeyCode.F)) //하나만 들 수 있다고 가정한 상태
        {
            PutDown();
        }

        if (isPickedUp && Input.GetKeyDown(KeyCode.Q))
        {

            PutInBag();

        }

        //플레이어가 뭔가 들고 있는 상태
        if (isCount && GameManager.manager.player.isPicking && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("쌓기를 시도합니다");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            isCount = true;
            //GameManager.manager.guideMessage.text = "스페이스를 눌러 들어올리기";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            {
                isCount = false;
                //GameManager.manager.guideMessage.text = "";
            }
        }
    }

    void PickedUp()
    {
        
        if(!GameManager.manager.player.isPicking)
        {
            Debug.Log("pickup");
            GameManager.manager.player.PickUp(this);
            this.transform.SetParent(GameManager.manager.player.transform);
            isPickedUp = true;
        }        

    }

    public void PutDown()
    {
       // Debug.Log("Set parent of Item to null");
        this.transform.parent = null;
        isPickedUp = false;
        GameManager.manager.player.DetechItem();
    }

    void PutInBag()
    {
        if (GameManager.manager.bag.Count < 3)
        {
            GameManager.manager.bag.Add(this);
            GameManager.manager.SetBagImage(itemCode);

            PutDown();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        } else
        {
            GameManager.manager.SetMessage("가방이 가득 찼습니다");
        }
    }
    void SetStack()
    {
        //플레이어가 들고 있는 아이템이 이 아이템 위로 온다.
        
    }
}
