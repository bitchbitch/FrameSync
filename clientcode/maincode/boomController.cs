using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boomController : MonoBehaviour
{

    // Use this for initialization
    float speed = 20f;
    float liveDis = 20;
    Transform trans;
    Numeric player;
    int flag;
    void Start()
    {
        trans = transform;
    }
    public void setPlayer(Numeric tmp)
    {
        player = tmp;
        flag = player.frame;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flag<=player.frame-20)
            Destroy(this.gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
        // Debug.Log(other.tag+" "+player.tagString);
        if (other.tag != player.tagString)
        {
            Numeric p2 = other.GetComponent<Numeric>();
            p2.HP -= player.ATK;
        }
    }
}

