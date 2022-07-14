using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public Transform getPoint;
    public bool isPicking;
    Item takedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;
        GetComponent<Rigidbody2D>().velocity = velocity; //벽에 부딪히면 0이 우선으로 들어간다?
                                                         //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;
        
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {

        GetComponent<Rigidbody2D>().gravityScale = 0;
        //Debug.Log("충돌");
        if (collision.name == "Item" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("누름");
            collision.gameObject.transform.SetParent(this.transform);
            collision.gameObject.transform.localPosition = getPoint.transform.localPosition;
            isPicking = true;
        }
        //충돌한 정보는 아이템이 가지고 있는 게 낫다. 플레이어가 하나고 아이템이 여러개이기 때문에?
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void PickUp(Item picked)
    {
        takedObject = picked;
        Debug.Log("플레이어가 " + takedObject + "를 주웠다");
        isPicking = true;
    }

    //내려놓기

    public void DetechItem()
    {
        takedObject = null;
        isPicking = false;
        
    }
   
}
