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
    public LayerMask layerMask;
    public LayerMask layerMaskForPick;
    public GameObject point;
    int count;
    int forward;
    float charge;
    public GameObject red;
    public float chargeSpeed;
    public GameObject pupil;
   
    // Start is called before the first frame update
    void Start()
    {
        forward = 1;
        charge = 1.5f;
        point.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        /*Color color = red.GetComponent<SpriteRenderer>().color;
        red.GetComponent<SpriteRenderer>().color.a = charge / 20;*/


        var velocity = Vector2.zero;
        if (/*isOnGround*/ true)
        {
            velocity.x = Input.GetAxisRaw("Horizontal") * speed;
            velocity.y = Input.GetAxisRaw("Vertical") * speed;

            if (Input.GetAxisRaw("Horizontal") < 0)
                forward = -1;
                
            else if (Input.GetAxisRaw("Horizontal") > 0)
                forward = 1;


            transform.localScale = new Vector3(forward, 1, 1);
            
        }
        GetComponent<Rigidbody2D>().velocity = velocity; //벽에 부딪히면 0이 우선으로 들어간다?
                                                         //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;



        
        Vector2 rayPosition = new Vector2(transform.position.x + (forward * charge) , 10);
        Ray2D ray = new Ray2D(rayPosition, Vector2.down);
        Debug.DrawRay(rayPosition, Vector2.down * 20, Color.green);
        RaycastHit2D hitData = Physics2D.Raycast(rayPosition, Vector2.down, 20, layerMask);
        var dropPoint = hitData.point;

        //pupil.transform.position += new Vector3(hitData.point.normalized.x, hitData.point.normalized.y, 0) * Time.deltaTime;


        Ray2D rayForPick = new Ray2D(transform.position, new Vector2(forward, -1));
        Debug.DrawRay(transform.position, new Vector2(forward, 0), Color.red);
        RaycastHit2D hitDataForPick = Physics2D.Raycast(transform.position, new Vector2(forward, -1), 1, layerMaskForPick);
        Debug.Log(hitDataForPick.point);
        








        if (hitDataForPick && !isPicking && Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("F key Pressed for " + hitDataForPick.transform.name);
            PickUp(hitDataForPick.collider.GetComponent<Item>());
            Debug.Log(hitDataForPick.collider.GetComponent<Item>());
        }
        else if (isPicking && Input.GetKey(KeyCode.F))
        {
            if(point.activeSelf == false)
                point.SetActive(true);
            charge += Time.deltaTime * chargeSpeed;
            point.transform.position = hitData.point + Vector2.up;
        }


        else if (isPicking && Input.GetKeyUp(KeyCode.F)) //하나만 들 수 있다고 가정한 상태
        {

            takedItem.transform.position = new Vector3(dropPoint.x, dropPoint.y + 2f, 0);
            takedItem.PutDown();
            charge = 1.5f;
            point.SetActive(false);
        }

        if (isPicking && Input.GetKeyDown(KeyCode.Q))
        {
            if(takedItem.isBagPossible)
            {
                GameManager.manager.bag.PutInBag(takedItem);
            }

        }
        if (!isPicking && Input.GetKeyDown(KeyCode.E))
        {
            var item = GameManager.manager.bag.GetOutOfBag();
            //GetOutOfBag();
            //가방에 있는걸 takedObject로 부른다.

            if (item)
            {
                item.gameObject.SetActive(true);
                PickUp(item);
                //item.PickedUp();
                item.transform.localPosition = new Vector3(0, 1, 0);
            }
            

        }

    }


  



    private void OnTriggerExit2D(Collider2D collision)
    {
        counted = collision.gameObject;
        //GetComponent<Rigidbody2D>().gravityScale = 1;

        if (collision.gameObject.name == "Item")
        {
            {
                countedItem = null;
                isCountItem = false;
            }
        }
        
    }


    public void PickUp(Item picked)
    {
        picked.PickedUp();
        takedItem = picked;
        Debug.Log("플레이어가 " + takedItem + "를 주웠다");
        isPicking = true;
    }

    //내려놓기

    public void DetechItem()
    {
        takedItem.GetComponent<Rigidbody2D>().isKinematic = false;
        takedItem.transform.parent = null;
        takedItem.gameObject.layer = 6;
        takedItem.GetComponent<Item>().isPickedUp = false;

        takedItem = null;
        isPicking = false;
        
    }
   
}
