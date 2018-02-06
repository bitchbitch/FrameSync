using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace AssemblyCSharp
{
	public class ClientSocket
	{
		private static byte[] result = new byte[65535];
		private static Socket clientSocket;
		private static ClientSocket client = new ClientSocket();
		//是否已连接的标识
		public bool IsConnected = false;
		private ClientSocket(){
			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}
		public static ClientSocket GetSocket(){
			return client;
		}
		public void ClientClose()
		{
			clientSocket.Shutdown(SocketShutdown.Both);
			clientSocket.Close();
		}
		/// <summary>
		/// 连接指定IP和端口的服务器
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>
		public void ConnectServer(string ip,int port)
		{
			IPAddress mIp = IPAddress.Parse(ip);
			IPEndPoint ip_end_point = new IPEndPoint(mIp, port);

			try {
				clientSocket.Connect(ip_end_point);
				IsConnected = true;
				Debug.Log("连接服务器成功");
			}
			catch
			{
				IsConnected = false;
				Debug.Log("连接服务器失败");
				return;
			}
			//服务器下发数据长度
			/*
			int receiveLength = clientSocket.Receive(result);
			ByteBuffer buffer = new ByteBuffer(result);
			int len = buffer.ReadShort();
			string data = buffer.ReadString();
			Debug.Log("服务器返回数据：" + data);
			*/
		}
		public string ReceiveMessage()
		{
			int receiveLength = clientSocket.Receive (result);
			ByteBuffer buffer = new ByteBuffer (result);
			ushort len = buffer.ReadShort ();
			//Debug.Log (len);
			string data = buffer.ReadString ();
			//Debug.Log (count);
			return data;
		}
		public string ReceivePack()
		{
			int receiveLength = clientSocket.Receive (result);
            int fuckbitch = receiveLength;
			ByteBuffer buffer = new ByteBuffer (result);
			string ans = "";
            int bitch = 0;
			while (receiveLength > 0) {
				int len = buffer.ReadShort ();
				//Debug.Log (len.ToString()+" "+receiveLength.ToString());
				receiveLength = receiveLength-len-2;
				string frame = buffer.ReadString ();
                //Debug.Log(fuckbitch.ToString()+" "+receiveLength.ToString()+" "+ bitch.ToString()+" "+frame);
				len -= (frame.Length+2);
				string data = frame+"#";
				while (len > 0) {
					int slen = buffer.ReadShort ();
					//Debug.Log (slen.ToString () + "fuck" + len.ToString ());
					data += buffer.ReadString ();
					len = len - slen - 2;
				}
				ans += (data+'$');
                bitch++;
			}
            //Debug.Log(fuckbitch.ToString()+" "+bitch.ToString());
			return ans;
		}
		/// <summary>
		/// 发送数据给服务器
		/// </summary>
		public void SendMessage(string data)
		{
			if (IsConnected == false)
				return;
			try
			{
				ByteBuffer buffer = new ByteBuffer();
				buffer.WriteString(data);
				clientSocket.Send(WriteMessage(buffer.ToBytes()));
			}
			catch
			{
				IsConnected = false;
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
			}
		}

		/// <summary>
		/// 数据转换，网络发送需要两部分数据，一是数据长度，二是主体数据
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private static byte[] WriteMessage(byte[] message)
		{
			MemoryStream ms = null;
			using (ms = new MemoryStream())
			{
				ms.Position = 0;
				BinaryWriter writer = new BinaryWriter(ms);
				int x = message.Length+2;
				//Debug.Log (x);
				//Debug.Log (x & 11111111);
				// x = (((x & (0xFF)) << 8) | (x >> 8));
				//Debug.Log (x.ToString());
				ushort msglen = (ushort)x;
				//Debug.Log (msglen);
				writer.Write(msglen);
				writer.Write(message);
				writer.Flush();
				//Debug.Log(ms.ToArray().Length);
				return ms.ToArray();
			}
		}
	}
}

