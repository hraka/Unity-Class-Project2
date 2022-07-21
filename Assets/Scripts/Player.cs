using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public Transform getPoint;
    public bool isPicking;
    public Item countedItem;
    public Item takedItem;
    private bool isCountItem;
    public bool isOnGround;
    public GameObject counted;
    int count;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = Vector2.zero;
        if (isOnGround)
        {
            velocity.x = Input.GetAxisRaw("Horizontal") * speed;
            velocity.y = Input.GetAxisRaw("Vertical") * speed;
            
        }
        GetComponent<Rigidbody2D>().velocity = velocity; //벽에 부딪히면 0이 우선으로 들어간다?
                                                         //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;


        if (isCountItem && Input.GetKeyDown(KeyCode.K))
        {
            
            Debug.Log("접촉" + count++);
        }

        if (isCountItem && isPicking && Input.GetKeyDown(KeyCode.Space))
        {
            countedItem.SetStack();
        }
        else if (isCountItem && Input.GetKeyDown(KeyCode.Space))
        {
            countedItem.PickedUp();
        }


        if (isPicking && Input.GetKeyDown(KeyCode.F)) //하나만 들 수 있다고 가정한 상태
        {
            takedItem.PutDown();
        }

        if (isPicking && Input.GetKeyDown(KeyCode.Q))
        {

            GameManager.manager.bag.PutInBag(takedItem);

        }
        if (!isPicking && Input.GetKeyDown(KeyCode.E))
        {
            var item = GameManager.manager.bag.GetOutOfBag();
            //GetOutOfBag();
            //가방에 있는걸 takedObject로 부른다.

            item.gameObject.SetActive(true);
            item.PickedUp();
            item.transform.localPosition = new Vector3(0, 1, 0);

        }

    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        counted = collision.gameObject;
        

        if (collision.gameObject.name == "Item")
        {
            countedItem = collision.GetComponent<Item>();
            isCountItem = true;
            
        }



    }

    private void OnTriggerStay2D(Collider2D collision) //프레임마다 호출
    {
        if (collision.transform.tag == "Ground")
        {
            Debug.Log("땅에 닿음");
            isOnGround = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        counted = collision.gameObject;
        GetComponent<Rigidbody2D>().gravityScale = 1;

        if (collision.gameObject.name == "Item")
        {
            {
                countedItem = null;
                isCountItem = false;
            }
        }
        if (collision.transform.tag == "Ground")
        {
            Debug.Log("땅에서 나감");
            isOnGround = false;
        } //다른 땅 타일로 옮겨갈 때 움직이지 않는다. 땅에 접촉했는지 여부?
    }

    public void PickUp(Item picked)
    {
        takedItem = picked;
        Debug.Log("플레이어가 " + takedItem + "를 주웠다");
        isPicking = true;
    }

    //내려놓기

    public void DetechItem()
    {
        takedItem.transform.parent = null;
        takedItem.GetComponent<Item>().isPickedUp = false;

        takedItem = null;
        isPicking = false;
        
    }
   
}
