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
    public bool isDependent;
    public Item groundItem;
    bool isGrounded;
    public Item justUnderItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if(!isGrounded && !GameManager.manager.player.isPicking)
        {
            Debug.Log("pickup");
            GameManager.manager.player.PickUp(this);
            this.transform.SetParent(GameManager.manager.player.transform);
            isPickedUp = true;

            if(isDependent)
            {
                groundItem.stackCount -= 1;
            }
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
        if(isDependent) //�� �������� ���� �������̸� �� �Ʒ� �������� �Լ��� ȣ��
        {

            groundItem.SetStack(); //�� �Ʒ��� �ִ� �������� ground�� ����.
            
        } else
        {
            Vector3 randomPosition = new Vector3(Random.Range(-0.3f, 0.3f), ++stackCount, 0);
            GameManager.manager.player.takedItem.transform.position = this.transform.position + randomPosition;
            isGrounded = true;
            GameManager.manager.player.takedItem.isDependent = true;
            GameManager.manager.player.takedItem.groundItem = this;
            GameManager.manager.player.DetechItem();
        }


    }
}
