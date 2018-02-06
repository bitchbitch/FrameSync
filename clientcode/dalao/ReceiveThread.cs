using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


using AssemblyCSharp;

public class ReceiveThread
{
	// Use this for initialization
	static Thread thread;
	static ClientSocket clientsocket = ClientSocket.GetSocket ();
	static ReceiveThread receivThread = new ReceiveThread ();

	public static void start ()
	{
		thread = new Thread (new ThreadStart (receive));
		thread.Start ();
	}
	
	// Update is called once per frame
	static void receive()
	{
		//Debug.Log ("fuck");
		while (true) {
			string shit = clientsocket.ReceivePack ();
			//Debug.Log (shit);
			dePack (shit);
		}
	}
	static void dePack (string shit){
		//Debug.Log (shit.Length);
		int x = 0;
		int count = 0;
		string tmp="";
        int t=3;
        string T="1";
        Vector3 v3 = Vector3.zero;
        int bitch = 0;

		while (x < shit.Length) {
			while(shit[x]!='#'){
				tmp = tmp + shit [x];
				x++;
			}
			int frame = int.Parse (tmp);
//			Debug.Log (frame);
			x++;tmp = "";
			count = 0;
            //Debug.Log (x.ToString()+" "+shit.Length.ToString());
            bitch++;
			while (shit[x] != '$') {
				while (shit [x] != ':' && shit [x] != '#') {
					tmp = tmp + shit [x];
					x++;
				}
				//Debug.Log (tmp);
				x++;count++;
				//if (fuck >= 1)	Debug.Log (tmp + "fuck");
				if (count == 2) {
					//Debug.Log (message.type);
					t = int.Parse(tmp);
				}
				else if (count == 3) {
					T = tmp;
					//if (bitch > 1)Debug.Log ("3 "+tmp+" "+shit);
					//Debug.Log (message.Type);
				} else if (count == 4) {
					v3.x = float.Parse (tmp);
				} else if (count == 5) {
					v3.y = float.Parse (tmp);
				}else if(count==6){
					v3.z=float.Parse(tmp);
                    Message message = new Message()
                    {
                        type = t,
                        frame = frame,
                        Type = T,
                        v3 = v3
                    };
					//if(message.type==5)Debug.Log(message.Type+" "+message.type+" "+shit);
                    //Queue<Message> tmpp = MessageQueue.debug();
					MessageQueue.put (message); 
					//if (fuck > 1)	Debug.Log(message.Type+" "+message.type+" "+shit);
					//if (fuck >= 1&&(message.type==5)) Debug.Log (message.type);
					count = 0;
				}
				tmp = "";
			}
			if(shit[x]=='$') x++;
			//bitch++;
		}
        //Debug.Log(bitch.ToString() + " " + shit);
	}
	public static void close()
	{
		if (thread != null)
			thread.Abort ();
	}
}

