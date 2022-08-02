using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

    private Camera cam;
    public GameObject pupil;
    public Vector3 dir;
    public Vector3 newDir;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 방향을 향해 이동한다.

        Debug.Log(Camera.main.ScreenToWorldPoint(Vector3.up));
        dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        newDir = new Vector3(dir.x, dir.y, 0);
        //transform.position += dir * Time.deltaTime;
       

    }
}
