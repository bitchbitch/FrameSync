using UnityEngine;
using System.Collections;

public class ST 
{

	// Use this for initialization
	public int P=1;
    public string result="你被击败啦";
	public static ST instance_ = new ST();
	private ST(){
	}

	public static ST getInstance()
	{
		return instance_;
	}
}

