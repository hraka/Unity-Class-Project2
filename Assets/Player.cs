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
        GetComponent<Rigidbody2D>().velocity = velocity; //���� �ε����� 0�� �켱���� ����?
                                                         //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;
        
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {

        GetComponent<Rigidbody2D>().gravityScale = 0;
        //Debug.Log("�浹");
        if (collision.name == "Item" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("����");
            collision.gameObject.transform.SetParent(this.transform);
            collision.gameObject.transform.localPosition = getPoint.transform.localPosition;
            isPicking = true;
        }
        //�浹�� ������ �������� ������ �ִ� �� ����. �÷��̾ �ϳ��� �������� �������̱� ������?
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void PickUp(Item picked)
    {
        takedObject = picked;
        Debug.Log("�÷��̾ " + takedObject + "�� �ֿ���");
        isPicking = true;
    }

    //��������

    public void DetechItem()
    {
        takedObject = null;
        isPicking = false;
        
    }
   
}
