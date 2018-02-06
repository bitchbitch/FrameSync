using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyController : MonoBehaviour {

    public Vector3 initPosition;
    //虚拟方向按钮可移动的半径  
    public float r;
    //border对象  
    public RectTransform border;
    Vector3 dir;
    Vector3 tmpPosition;
    Transform trans;
    float angel = -22.3845f;
    Numeric num;
    bool isdrag = false;
    // Use this for initialization
    void Start () {
        initPosition = GetComponentInParent<RectTransform>().position;
        num = GameObject.FindWithTag("Player").GetComponent<Numeric>();
        trans = transform;
        GameObject joy = GameObject.Find("ControllerBackground");
        var r2 = joy.GetComponent<RectTransform>();
        r = ( r2.rect.width - border.rect.width) / 2;
        StartCoroutine(getInput());
    }
    /* private void FixedUpdate()
     {
             Vector3 moveDirection = new Vector3(dir.normalized.x, 0, dir.normalized.y) * Mathf.Cos(angel);
             moveDirection = -moveDirection * num.speed * 0.03f;
             Message fuck = new Message()
             {
                 type = 3,
                 Type = num.id.ToString(),
                 v3 = moveDirection
             };
             SendThread.put(fuck);

     }*/
    IEnumerator getInput()
    {
        while (true)
        {
            //print ("fuck");
            Vector3 moveDirection = new Vector3(dir.normalized.x, 0, dir.normalized.y) * Mathf.Cos(angel);
            moveDirection = -moveDirection * num.speed * 0.06f;
            Message fuck = new Message()
            {
                type = 3,
                Type = num.id.ToString(),
                v3 = moveDirection
            };
            SendThread.put(fuck);
            yield return new WaitForSeconds(0.03f);
        }
    }

    // Update is called once per frame
    public void onDrag()
    {
        if (Vector3.Distance(Input.mousePosition, initPosition) < r)
        {
            //虚拟键跟随鼠标  
            trans.position = Input.mousePosition;
            dir = (Input.mousePosition - initPosition);
        }
        else
        {
            //计算出鼠标和原点之间的向量  
            dir = Input.mousePosition - initPosition;
            //这里dir.normalized是向量归一化的意思，实在不理解你可以理解成这就是一个方向，就是原点到鼠标的方向，乘以半径你可以理解成在原点到鼠标的方向上加上半径的距离  
            trans.position = initPosition + dir.normalized * r;
        }
        isdrag = true;
    }
    public void onEndDrag()
    {
        trans.position = initPosition;
        isdrag = false;
        dir = Vector3.zero;
    }
}
