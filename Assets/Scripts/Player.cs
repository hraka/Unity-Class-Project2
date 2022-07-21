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
        GetComponent<Rigidbody2D>().velocity = velocity; //���� �ε����� 0�� �켱���� ����?
                                                         //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;


        if (isCountItem && Input.GetKeyDown(KeyCode.K))
        {
            
            Debug.Log("����" + count++);
        }

        if (isCountItem && isPicking && Input.GetKeyDown(KeyCode.Space))
        {
            countedItem.SetStack();
        }
        else if (isCountItem && Input.GetKeyDown(KeyCode.Space))
        {
            countedItem.PickedUp();
        }


        if (isPicking && Input.GetKeyDown(KeyCode.F)) //�ϳ��� �� �� �ִٰ� ������ ����
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
            //���濡 �ִ°� takedObject�� �θ���.

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

    private void OnTriggerStay2D(Collider2D collision) //�����Ӹ��� ȣ��
    {
        if (collision.transform.tag == "Ground")
        {
            Debug.Log("���� ����");
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
            Debug.Log("������ ����");
            isOnGround = false;
        } //�ٸ� �� Ÿ�Ϸ� �Űܰ� �� �������� �ʴ´�. ���� �����ߴ��� ����?
    }

    public void PickUp(Item picked)
    {
        takedItem = picked;
        Debug.Log("�÷��̾ " + takedItem + "�� �ֿ���");
        isPicking = true;
    }

    //��������

    public void DetechItem()
    {
        takedItem.transform.parent = null;
        takedItem.GetComponent<Item>().isPickedUp = false;

        takedItem = null;
        isPicking = false;
        
    }
   
}
