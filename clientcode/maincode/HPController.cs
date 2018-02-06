using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour {
    
	Numeric player;
    Slider slider;

    void Start()
    {
		player = transform.parent.gameObject.GetComponent<Numeric>();
        slider = transform.FindChild("Slider").gameObject.GetComponent<Slider>();
        
        Vector3 vec = transform.parent.position;
        gameObject.transform.position = transform.parent.position + new Vector3(0, 4f, 0);
        vec = gameObject.transform.position;
    }
    
    void Update ()
    {
        slider.value = (1.0f * player.HP) / (player.initHP * 1.0f);
        transform.rotation = Camera.main.transform.rotation;
	}
}


