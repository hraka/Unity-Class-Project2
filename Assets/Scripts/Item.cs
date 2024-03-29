using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private bool isCount;
    private bool isFrontObject;
    public bool isPickedUp;
    public int itemCode;
    public int stackCount;
   
    public LayerMask layerMask;
    public bool isBagPossible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*  Ray ray = new Ray(transform.position, Vector3.up);
          Debug.DrawRay(transform.position, Vector3.up, Color.green);
          RaycastHit hitData;*/


        //플레이어가 아이템 앞에 서면
        //1) 플레이어가 아이템을 들고 있지 않은 경우 아이템을 든다.
        //2) 플레이어가 아이템을 들고 있는 경우, 아이템을 쌓는다.
        //두개가 겹쳐있는 경우 두 가지 일이 동시에 일어난다.



        //플레이어가 뭔가 들고 있는 상태
        if (isCount && GameManager.manager.player.isPicking && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("쌓기를 시도합니다");
        }
    }



    public void PickedUp()
    {
        
        if(/*!isGrounded &&*/ !GameManager.manager.player.isPicking)
        {

            
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.layer = 10;
            this.transform.SetParent(GameManager.manager.player.transform);
            isPickedUp = true;

            /*if(isDependent)
            {
                groundItem.stackCount -= 1;
            }*/
        }        
    }

    public void PutDown()
    {

        /*this.transform.parent = null;
        isPickedUp = false; //플레이어의 DetechItem으로 옮김*/
        
        GameManager.manager.player.DetechItem();
    }

    /*public void PutInBag()
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
    }*/
    public void SetStack()
    {

        //플레이어가 다른 아이템을 든 채로 이 아이템 위에서 조작을 하면
        //플레이어가 들고 있던 아이템이 이 아이템 위로 온다.

        //밑에 하나 이상의 아이템이 쌓여 있으면?

        //자기 자신 위에 아이템이 있으면?
        /*if(isDependent) //이 아이템이 쌓인 아이템이면 맨 아래 아이템의 함수를 호출
        {

            groundItem.SetStack(); //맨 아래에 있는 아이템이 ground에 들어간다.
            
        } else
        {

            //Ray ray = new Ray(new Vector3(transform.position.x, 10, transform.position.z), Vector3.down);
            //Debug.DrawRay(new Vector3(transform.position.x, 10, transform.position.z), Vector3.down, Color.green);

            Vector3 randomPosition = new Vector3(Random.Range(-0.3f, 0.3f), ++stackCount, 0);
            GameManager.manager.player.takedItem.transform.position = this.transform.position + randomPosition;
            isGrounded = true;
            GameManager.manager.player.takedItem.isDependent = true;
            GameManager.manager.player.takedItem.groundItem = this;
            
            GameManager.manager.player.DetechItem();
        }*/

        Ray2D ray = new Ray2D(new Vector2(transform.position.x, 10), Vector2.down);
        Debug.DrawRay(new Vector2(transform.position.x, 10), Vector2.down, Color.green);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(transform.position.x, 10), Vector2.down, 20, layerMask);
        Debug.Log(hitData.point);
        if(hitData.collider)
        {
            Debug.Log(hitData.collider.name);
        }
        Vector2 hitPosition = hitData.point;

        Vector3 randomPosition = new Vector3(Random.Range(-0.3f, 0.3f), hitPosition.y + 5f, 0);
        GameManager.manager.player.takedItem.transform.position = this.transform.position + randomPosition;

        GameManager.manager.player.DetechItem();


    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
