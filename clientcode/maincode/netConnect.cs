using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using AssemblyCSharp;

public class netConnect : MonoBehaviour {

	// Use this for initialization
	ClientSocket clientsocket = ClientSocket.GetSocket ();
	Thread listenServer;
	void Start () {
		clientsocket.ConnectServer ("10.0.128.244", 8088);
		//string tmp = 
		//listenServer = new Thread (reciveServer);
		//listenServer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void reciveServer(){
		while (true) {
			string rev = clientsocket.ReceivePack();
			//print (rev);
		}
	}
	void OnDisable()
	{
		//listenServer.Abort ();
	  	//clientsocket.ClientClose ();
	}
}
