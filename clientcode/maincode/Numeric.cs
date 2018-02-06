using UnityEngine;
using System.Collections;
using LRUwhite;

public class Numeric : MonoBehaviour
{

	// Use this for initialization
	public int HP;
	public int ATK;
	public BodyState bodyState;

	public int initHP;
	public bool[,] stateMap;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float turnspeed = 10;
	public Vector3 moveDirection =Vector3.zero;

	public int id=1;
    public string tagString;
    public int frame = 0;

	void Awake()
	{
		HP = initHP = TestRole.HP;
		ATK = TestRole.ATK;
		bodyState = TestRole.bodyState;
		stateMap = TestRole.stateMap;
	}

	void OnEnable()
	{
		HP = initHP;
	}
	public bool IsAlive()
	{
		return HP > 0;
	}
}

