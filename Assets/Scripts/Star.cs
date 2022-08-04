using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("star " + GameManager.manager);
        transform.localPosition = new Vector3(transform.position.x, GameManager.manager.targetHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Item")
        {
            GameManager.manager.UpperStar();
        }
    }
}
