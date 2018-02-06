using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour
{
	private Numeric player;
	private Vector3 offset;
	public float moveSpeed;

	void Awake()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Numeric> ();
		Vector3 shit= GameObject.FindWithTag ("Player").transform.position;
		//Vector3 shit = player.transform.localPosition;
		shit.y += 15;shit.z -=12;
		transform.localPosition = shit;
		//print (bitch.x);print (bitch.y);print (bitch.z);
		//Quaternion bitch = transform.localRotation;
		//print (bitch.x);
		offset = gameObject.transform.localPosition - player.transform.localPosition;
	}
	void Update()
	{
		Vector3 pos = player.transform.localPosition + offset;
		transform.localPosition = Vector3.Lerp (transform.localPosition, pos, player.speed*0.03f);
	}
}

