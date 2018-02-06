using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageQueue : MonoBehaviour
{
    static Queue<Message> msg;
    static char[] separator = { ':' };

	public int lastframe = 0;
	public int nowframe = 0x7f7f7f7f;
	Message shit = null;
    // Use this for initialization
    static MessageQueue()
    {
        msg = new Queue<Message>();
    }

    public static void put(Message message)
    {
		//if (message.type == 5)	Debug.Log (message.type);
		lock(msg){
			//Debug.Log (msg.Count);
			msg.Enqueue(message);
			//if (msg.Peek().type == 5)	Debug.Log ("ok");
		}
    }
    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
		/*if (shit != null){
			headleMessage (shit);
			shit = null;
		}
		if (msg.Count != 0) {
			shit = msg.Dequeue ();
			nowframe = shit.frame;
			while (msg.Count != 0) {
				shit = msg.Dequeue ();
				if (nowframe == shit.frame) {
					headleMessage (shit);
					shit = null;
				} else
					break;
			}
		}*/
		//lastframe = nowframe;
		//if(msg.Count>0)Debug.Log("update"+msg.Peek().type);
		while(msg.Count > 0) {
			lock (msg) {
				shit = msg.Dequeue ();
				//if(shit.type==5)Debug.Log (shit.type+" "+shit.Type);
			}
			headleMessage (shit);
		}
    }
    public static Queue<Message> debug()
    {
        return msg;
    }
    private static void headleMessage(Message message)
    {
        object[] obj = { message };
        EventSystem.Dispatch(message.Type, obj);
    }
}
