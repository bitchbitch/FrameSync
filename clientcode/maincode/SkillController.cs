using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillController : MonoBehaviour
{

	// Use this for initialization
	float speed = 20f;
	float liveDis = 20;
	Transform trans;
    Numeric player;
	void Start ()
	{
		trans = transform;
	}
    public void setPlayer(Numeric tmp)
    {
        player = tmp;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (liveDis < 0)
			Destroy (this.gameObject);
		liveDis -= Time.deltaTime*speed;
		trans.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
	}
    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
       // Debug.Log(other.tag+" "+player.tagString);
        if(other.tag != player.tagString)
        {
            Numeric  p2= other.GetComponent<Numeric>();
            p2.HP -= player.ATK;
        }
    }
}

