using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LRUwhite;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Use this for initialization

	void MakeObject(string tag,float rotate,float distance,Transform parent,Object prefab,ref GameObject obj)
	{
		obj = Instantiate (prefab as GameObject);
		obj.tag = tag;
		obj.transform.parent = parent;
		obj.transform.Rotate (new Vector3(0, rotate, 0));
		obj.transform.localPosition = new Vector3 (0, -1.7f, distance);
		obj.AddComponent<Numeric> ();
		GameObject HP = Instantiate(Red.HPPrefab as GameObject);
		HP.transform.SetParent (obj.transform);
	}
	void Awake()
	{
		//create obj
		string redTag="";
		string blueTag = "";
		ST pp = ST.getInstance();
		//print (pp.P);
		if (pp.P == 1) {
			redTag = "Player";
			blueTag = "P2";
		} else {
			redTag = "P2";
			blueTag = "Player";
		}
		Red.parent = GameObject.FindWithTag ("Red").transform;
		Red.yasuo = Resources.Load ("Prefabs/taunt", typeof(GameObject));
		Red.HPPrefab = Resources.Load ("Prefabs/HPCanvas", typeof(GameObject));
		GameObject yasuo = null;
		MakeObject (redTag, Red.rotate, Red.yasuodis, Red.parent, Red.yasuo, ref yasuo);
        Vector3 V1 = yasuo.transform.position; V1.z -= 10; yasuo.transform.position = V1;
        //yasuo.AddComponent<CharacterController> ();
        //yasuo.GetComponent<CharacterController> ().center = new Vector3(0,1,0);
        //yasuo.AddComponent<CC_test> ();
        //yasuo.AddComponent<Numeric> ();
        yasuo.GetComponent<Numeric>().tagString = redTag;


		Blue.parent = GameObject.FindWithTag ("Blue").transform;
		Blue.yasuo = Resources.Load ("Prefabs/taunt", typeof(GameObject));
		GameObject yasuo2 = null;
		MakeObject (blueTag,Blue.rotate, Blue.yasuodis, Blue.parent, Blue.yasuo, ref yasuo2);
        Vector3 V3=yasuo2.transform.position;V3.z += 10; yasuo2.transform.position = V3;
        //yasuo2.AddComponent<Numeric> ();
        yasuo2.GetComponent<Numeric> ().id = 2;
        yasuo2.GetComponent<Numeric>().tagString = blueTag;
        if (pp.P == 1) {
			yasuo2.AddComponent<P2> ();
			yasuo.AddComponent<PlayerController> ();
			//GameObject.FindWithTag ("MainCamera").transform = Red.parent;
		} else {
			yasuo.AddComponent<P2> ();
			yasuo2.AddComponent<PlayerController> ();
			//GameObject.FindWithTag ("MainCamera").transform = Blue.parent;
		}
				
		GameObject.FindWithTag ("MainCamera").AddComponent<MainCameraControl> ();
		//Application.runInBackground = true;
		//net
		SendThread.start();
		ReceiveThread.start ();

        GameObject  baseRed = null, baseBlue = null;
        GameObject redBartizan1 = null, redBartizan2 = null, redBartizan3 = null;
        GameObject blueBartizan1 = null, blueBartizan2 = null, blueBartizan3 = null;
        Red.baseHome = Resources.Load("Prefabs/Monster_base_red", typeof(GameObject));
        Red.bartizanPrefab = Resources.Load("Prefabs/Monster_bartizan_red", typeof(GameObject));
        Blue.baseHome = Resources.Load("Prefabs/Monster_base_blue", typeof(GameObject));
        Blue.bartizanPrefab = Resources.Load("Prefabs/Monster_bartizan_blue", typeof(GameObject));

        MakeObject("Untagged", Red.rotate, Red.bartDis0, Red.parent, Red.baseHome, ref baseRed);
        MakeObject("Untagged", Blue.rotate, Blue.bartDis0, Blue.parent, Blue.baseHome, ref baseBlue);
        //红塔
        MakeObject("Untagged", Red.rotate, Red.bartDis1, Red.parent, Red.bartizanPrefab, ref redBartizan1);
        MakeObject("Untagged", Red.rotate, Red.bartDis2, Red.parent, Red.bartizanPrefab, ref redBartizan2);
        MakeObject("Untagged", Red.rotate, Red.bartDis3, Red.parent, Red.bartizanPrefab, ref redBartizan3);
        //蓝塔
        MakeObject("Untagged", Blue.rotate, Blue.bartDis1, Blue.parent, Blue.bartizanPrefab, ref blueBartizan1);
        MakeObject("Untagged", Blue.rotate, Blue.bartDis2, Blue.parent, Blue.bartizanPrefab, ref blueBartizan2);
        MakeObject("Untagged", Blue.rotate, Blue.bartDis3, Blue.parent, Blue.bartizanPrefab, ref blueBartizan3);
    }
	void Start()
	{
		
		//SendThread.start();
	}
	void OnDisable()
	{
		ClientSocket clientsocket = ClientSocket.GetSocket ();
		clientsocket.ClientClose ();
	}
    private void Update()
    {
        Numeric _1p =GameObject.FindWithTag("Player").GetComponent<Numeric>();
        Numeric _2p = GameObject.FindWithTag("P2").GetComponent<Numeric>();
        if (_1p.HP <= 0) {
            ST pp = ST.getInstance();
            pp.result = "你被击败啦";
            SceneManager.LoadScene(2); }
        if (_2p.HP <= 0) {
            ST pp = ST.getInstance();
            pp.result = "你胜利啦";
            SceneManager.LoadScene(2); }
    }
}
