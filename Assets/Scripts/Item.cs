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


        //�÷��̾ ������ �տ� ����
        //1) �÷��̾ �������� ��� ���� ���� ��� �������� ���.
        //2) �÷��̾ �������� ��� �ִ� ���, �������� �״´�.
        //�ΰ��� �����ִ� ��� �� ���� ���� ���ÿ� �Ͼ��.



        //�÷��̾ ���� ��� �ִ� ����
        if (isCount && GameManager.manager.player.isPicking && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("�ױ⸦ �õ��մϴ�");
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
        isPickedUp = false; //�÷��̾��� DetechItem���� �ű�*/
        
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
            GameManager.manager.SetMessage("������ ���� á���ϴ�");
        }
    }*/
    public void SetStack()
    {

        //�÷��̾ �ٸ� �������� �� ä�� �� ������ ������ ������ �ϸ�
        //�÷��̾ ��� �ִ� �������� �� ������ ���� �´�.

        //�ؿ� �ϳ� �̻��� �������� �׿� ������?

        //�ڱ� �ڽ� ���� �������� ������?
        /*if(isDependent) //�� �������� ���� �������̸� �� �Ʒ� �������� �Լ��� ȣ��
        {

            groundItem.SetStack(); //�� �Ʒ��� �ִ� �������� ground�� ����.
            
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
