using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public Transform getPoint;
    public bool isPicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime * speed;


        if (Input.GetKeyDown(KeyCode.F) && isPicking)
        {

        }
        
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("충돌");
        if (collision.name == "Item" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("누름");
            collision.gameObject.transform.SetParent(this.transform);
            collision.gameObject.transform.localPosition = getPoint.transform.localPosition;
            isPicking = true;
        }
    }
}
