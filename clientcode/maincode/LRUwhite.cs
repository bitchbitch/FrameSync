using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LRUwhite{
	public class TestRole
	{
		//名字，血量，攻击力
		//移动速度，角色状态
		//初始位置，状态转换表
		public static int HP = 200;
		public static int ATK = 20;
		public static float moveSpeed = 10f;
		public static BodyState bodyState = BodyState.Idle;
		public static Vector3 initPosition = new Vector3(0, -1.3f, 0);
		public static bool[,] stateMap = {
			{true,  true,  true,  true,  true,  true,  true},
			{true,  false,  true,  true,  true,  true,  true},
			{false, false, true, false, false, false, false},
			{false, false, false,  false, false, false, false},
			{false, false, false,  false, false, false, false},
			{false, false, false,  false, false, false, false},
			{false, false, false,  false, false, false, false}
		};
	}



	public class Skill
	{
		public int lifeTime;
		public int playTime;
		public float skillMoveSpeed;
		public float changeRoleMoveSpeed;
		public Vector3 offsetOfRole;
		public Skill(int l, int p, float s, float c, Vector3 o)
		{
			lifeTime = l;
			playTime = p;
			skillMoveSpeed = s;
			changeRoleMoveSpeed = c;
			offsetOfRole = o;
		}
	}
	public class TestSkill
	{
		public static int id;
		public static Skill[] Skills = {
			new Skill(3, 2, 0f, 10f, Vector3.zero),      //Run
			new Skill(60, 20, 0f, 0f, Vector3.zero),      //Comatose
			new Skill(60, 24, 0f, 0f, Vector3.zero),     //Attack
			new Skill(180, 32, 0.4f, 0f, new Vector3(0, 0.5f, 1.5f)),      //Skill1
			new Skill(3, 23, 0f, 20f, Vector3.zero),     //Skill2
			new Skill(60, 24, 0f, 0f, Vector3.zero),      //Skill3
		};
	}

	public static class TestBuff
	{
		public static string name;
		public static bool isSuperArmor;
		public static int HPBuff;
		public static int MPBuff;
		public static float ATKBuff;
		public static float moveSpeedBuff;
		public static float liftTime;
	}

	public static class Red
	{
		public static Transform parent;
		public static Object baseHome;
		public static Object bartizanPrefab;

		public static float rotate = 0;
		public static float bartDis0 = 0;
		public static float bartDis1 = 41f;
		public static float bartDis2 = 25.6f;
		public static float bartDis3 = 16f;


		public static float yasuodis = -0f;
		public static Object yasuo;
		public static Object HPPrefab;
	}

	public static class Blue
	{
		public static Transform parent;
		public static Object baseHome;
		public static Object bartizanPrefab;
		public static Object yasuo;

		public static float yasuodis = -0f;
		public static float rotate = 0;
		public static float bartDis0 = 0;
		public static float bartDis1 = -42.6f;
		public static float bartDis2 = -27f;
		public static float bartDis3 = -17.5f;

	}
}