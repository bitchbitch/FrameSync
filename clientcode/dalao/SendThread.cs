using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using AssemblyCSharp;

public class SendThread {

    static Thread thread;
	static ClientSocket clientsocket = ClientSocket.GetSocket();
    static BlockQueue<Message> blockQueue = new BlockQueue<Message>(65535);

    public static void start()
    {
        thread = new Thread(new ThreadStart(send));
        thread.Start();
    }
    static void send()
    {
        while (true)
        {
            Message tmp = front();
			if (tmp == null)
				continue;
			string shit = ":"+tmp.type.ToString()+":"+tmp.Type.ToString () + ":" + tmp.v3.x.ToString () + ":" + tmp.v3.y.ToString () + ":" + tmp.v3.z.ToString ()+"#";
			//Debug.Log (shit.Length);
			//Debug.Log (msg.Count);
			clientsocket.SendMessage(shit);
        }
    }
    public static void close()
    {
        if(thread!=null)
            thread.Abort();
    }
    public static void put(Message item)
    {
        if (thread == null)
        {
            Debug.Log("send thread is not start");
            return;
        }
        blockQueue.EnQueue(item);
    }
    public static Message front()
    {
        return blockQueue.DeQueue();
    }
}
