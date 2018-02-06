using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using AssemblyCSharp;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	public Button yourbtn;
	public Text content;
	private IEnumerator _launcher = null;
	//ClientSocket clientsocket = ClientSocket.GetSocket();
	void Start () {
		Button btn = yourbtn.GetComponent<Button> ();
		btn.onClick.AddListener(TaskOnClink);
	}
	void TaskOnClink(){
		//string tmp = ":" + content.text;
		//clientsocket.SendMessage (tmp);
		string tmp=content.text;
		ST pp = ST.getInstance ();
		pp.P = int.Parse (tmp);
		SceneManager.LoadScene (1);
	}
	// Update is called once per frame
}
