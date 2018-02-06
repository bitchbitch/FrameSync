using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    // Use this for initialization
    int liveTime = 0;
    Numeric player;
    void Start () {
		
	}

    // Update is called once per frame
    public void setPlayer(Numeric tmp)
    {
        player = tmp;
    }
    void FixedUpdate()
    {
        liveTime++;
        if (liveTime > 10)
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
