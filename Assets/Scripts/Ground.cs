using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Player")
        {
            Debug.Log("∂•ø° ¥Í¿Ω");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.transform.name == "Player")
        {
            Debug.Log("∂•ø° ¥Í¿Ω");
        }*/
    }
}
