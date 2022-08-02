using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeing : MonoBehaviour
{

    private Camera cam;
    public GameObject pupil;
    public Vector3 dir;
    public Vector3 newDir;
    public Vector3 localInitPoint;

    public RectTransform transform_target;

    // Start is called before the first frame update
    void Start()
    {
        localInitPoint = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 방향을 향해 이동한다.

        var point = this.transform.position;

        dir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        newDir = new Vector3(dir.x, dir.y, 0) - this.transform.position;


        float deltaX = point.x - pupil.transform.position.x;
        float deltaY = point.y - pupil.transform.position.y;

        float temp = Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2);
        float result = Mathf.Sqrt(temp); //현재 눈동자 위치와 중심까지의 반지름

        Debug.Log("r = " + result);
        

        if((int)result >= 1) //
        {

            
            Debug.Log("범위초과");
        } else
        {
            pupil.transform.position += newDir.normalized * Time.deltaTime;
        }

        








    }
}
