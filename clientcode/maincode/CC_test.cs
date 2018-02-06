using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CC_test : MonoBehaviour {

	// Use this for initialization
	Animator animator;
	CharacterController cc_test ;
	Numeric num;

	//移动属性
	float speed;
	float jumpSpeed;
	float gravity;
	float turnspeed;
	Vector3 moveDirection =Vector3.zero;

	//about aminator
	Object[] skillPrefab = new Object[6];
	AnimatorStateInfo attack;

	void Start () {
		cc_test = GetComponent<CharacterController> ();
		animator = gameObject.GetComponent<Animator> ();
		num = gameObject.GetComponent<Numeric> ();
		init ();
		skillPrefab[1] = Resources.Load("Prefabs/Player_sorceress_attack_bingzhupenshe_Clip01", typeof(GameObject));
		/*skillPrefab[2] = Resources.Load("Prefabs/Skill/YujiSkillA", typeof(GameObject));
		skillPrefab[3] = Resources.Load("Prefabs/Skill/YujiSkill1", typeof(GameObject));
		skillPrefab[4] = Resources.Load("Prefabs/Skill/YujiSkill2", typeof(GameObject));
		skillPrefab[5] = Resources.Load("Prefabs/Skill/YujiSkill3", typeof(GameObject));*/
		Button btn = GameObject.Find ("Attack").GetComponent<Button> ();
		btn.onClick.AddListener (onAttack);
		btn = GameObject.Find ("Skill1").GetComponent<Button> ();
		btn.onClick.AddListener (Skill1);
		btn = GameObject.Find ("Skill2").GetComponent<Button> ();
		btn.onClick.AddListener ( Skill2 );
		btn = GameObject.Find ("Skill3").GetComponent<Button> ();
		btn.onClick.AddListener ( Skill3 );
	}
	void init()
	{
		speed = num.speed;
		jumpSpeed = num.jumpSpeed;
		gravity = num.gravity;
		turnspeed = num.turnspeed;
	}
	// Update is called once per frame
	bool isSkill1=false;
	void Update () {
		attack = animator.GetCurrentAnimatorStateInfo (0);
		if (isSkill1)
			print("fuck");
		else	Move ();
		if (!attack.IsName ("Idle2") && !attack.IsName ("run1") && attack.normalizedTime > 0.9f) {
			animator.SetInteger ("attack", 0);
			animator.SetInteger ("Skill2", 0);
			animator.SetInteger ("Skill1", 0);
			isSkill1 = false;
		}
		//Attack ();
	}
	void Move(){
		if (cc_test.isGrounded) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection *= speed;
			//print (moveDirection.x.ToString()+":"+moveDirection.y.ToString()+":"+moveDirection.z.ToString());
			transform.LookAt (transform.position + moveDirection);
			if (Input.GetKeyDown (KeyCode.Space))
				moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		if (moveDirection.x != 0  || moveDirection.z != 0) {
			animator.SetInteger ("attack", 0);
			animator.SetInteger ("Skill2", 0);
			animator.SetInteger ("Skill1", 0);
			animator.SetInteger ("condition", 1);
		}
		else
			animator.SetInteger ("condition", 0);
		//num.moveDirection = moveDirection ;
		cc_test.Move (moveDirection * Time.deltaTime);
	}
	void Attack(){
		if (attack.IsName ("Idle2") || attack.IsName ("run1"))
			animator.SetInteger ("attack", 0);
		if (Input.GetKeyDown (KeyCode.J)) {
			if(!attack.IsName("attack"))
				animator.SetInteger ("attack", 1);
			else if(animator.GetInteger("attack")==1)
				animator.SetInteger ("attack", 2);
			else if(animator.GetInteger("attack")==2)
				animator.SetInteger ("attack", 3);
			else if(animator.GetInteger("attack")==3)
				animator.SetInteger ("attack", 4);
		}
	}
	void onAttack(){
		print ("fuck");
		if ((attack.IsName ("Idle2") || attack.IsName ("run1")))
			animator.SetInteger ("attack", 1);
		else if (attack.IsName ("attack1") && attack.normalizedTime > 0.65f)
			animator.SetInteger ("attack", 2);
		else if (attack.IsName ("attack2") && attack.normalizedTime > 0.65f)
			animator.SetInteger ("attack", 3);
		else if (attack.IsName ("attack3") && attack.normalizedTime > 0.65f)
			animator.SetInteger ("attack", 4);
	}
	void Skill1(){
		animator.SetInteger ("condition", 0);
		isSkill1 = true;
		animator.SetInteger ("Skill1", 1);
		Invoke ("newBullet", 0.8f);
	}
	void Skill2(){
		animator.SetInteger ("Skill2", 1);
	}
	void Skill3(){
	}
	void newBullet(){
		GameObject obj = Instantiate (skillPrefab [1] as GameObject);
		//GameObject player = GameObject.FindWithTag ("Player");
		obj.transform.rotation = transform.rotation;
		obj.transform.position = transform.position;
	}
}
