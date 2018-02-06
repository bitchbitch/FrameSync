using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_log : MonoBehaviour {

	// Use this for initialization
	public Text text;
	public Text text2;
    public Text text3;
	GameObject P1;
	GameObject P2;
	void Start () {
		P1 = GameObject.FindWithTag ("Player");
		P2 = GameObject.FindWithTag ("P2");
	}
	
	// Update is called once per frame
	void Update () {
		text.text = P1.transform.localPosition.x.ToString () + "#" + P1.transform.localPosition.z.ToString ();
		text2.text = P2.transform.localPosition.x.ToString()+"#"+P2.transform.localPosition.z.ToString();
        text3.text = "P1*" + P1.GetComponent<PlayerController>().P1tim.ToString() + " P2*" + P2.GetComponent<P2>().P2tim.ToString();

    }
}
