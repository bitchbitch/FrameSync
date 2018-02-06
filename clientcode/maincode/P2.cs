using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LRUwhite;

public class P2 : MonoBehaviour
{

	// Use this for initialization
	Animator animator;
	CharacterController cc_test;
	Numeric num;

	float speed;
	float jumpSpeed;
	float gravity;
	float turnspeed;
	Vector3 moveDirection =Vector3.zero;

	AnimatorStateInfo attack;
    public int P2tim = 0;

    int isSkill1 = 0;
    int isattack = 0;
    int attack_count = 0;
    int nowframe = 0;
    int isMove = 0;

    Object[] skillPrefab = new Object[6];

	void Start (){
		cc_test = GetComponent<CharacterController> ();
		animator = gameObject.GetComponent<Animator> ();
		num = gameObject.GetComponent<Numeric> ();
		init ();
        //print (num.id);

        skillPrefab[0] = Resources.Load("Prefabs/Monster_alfredo_attack1_Clip01", typeof(GameObject));
		skillPrefab[1] = Resources.Load("Prefabs/Player_sorceress_attack_bingzhupenshe_Clip01", typeof(GameObject));
        skillPrefab[2] = Resources.Load("Prefabs/Player_warrior_attack_jianta_Clip01", typeof(GameObject));
        skillPrefab[3] = Resources.Load("Prefabs/Player_warrior_attack_lianguzhuangji_ex_Clip02", typeof(GameObject));

        EventSystem.Register (num.id.ToString(), fuckMessage);
		//StartCoroutine(getInput());
	}
	void init()
	{
		speed = num.speed;
		jumpSpeed = num.jumpSpeed;
		gravity = num.gravity;
		turnspeed = num.turnspeed;
	}
    void initData()
    {
        speed = num.speed;
        jumpSpeed = num.jumpSpeed;
        gravity = num.gravity;
        turnspeed = num.turnspeed;
    }
    private void fuckMessage(object[] obj)
    {
        attack = animator.GetCurrentAnimatorStateInfo(0);
        Message message = (Message)obj[0];
        nowframe = message.frame;
        P2tim++;
        num.frame = nowframe;
        //if(message.type==5)	Debug.Losg (message.type);
        //Debug.Log(message.frame.ToString() + " " + isattack.ToString());
        //string shit = message.v3.x.ToString()+" "+message.v3.z.ToString(); 
        //Logout.Log ("C:\\Users\\btchbtchliu\\Downloads", shit);
        if (message.frame - isSkill1 > 30)
            {
                if (message.type == 3)
                {
                    Vector3 tmp = message.v3;
                    //print (tmp.x);
                    Move(tmp);
                }
                else if (message.type == 4)
                {
                    onAttack();
                }
                else if (message.type == 5)
                {
                    //Debug.Log (message.type);
                    Skill1();
                    isSkill1 = message.frame;
                }
                else if (message.type == 6)
                {
                    Skill2();
                }
                else if (message.type == 7)
                {
                    Skill3();
                }
            }
            /*if (!attack.IsName ("Idle2") && !attack.IsName ("run1") && attack.normalizedTime > 0.9f) {
                animator.SetInteger ("attack", 0);
                animator.SetInteger ("Skill2", 0);
                animator.SetInteger ("Skill1", 0);
                //isSkill1 = false;
            }*/
        }
        void Move(Vector3 moveDirection)
        {
            if (cc_test.isGrounded)
            {
                //print ("fuck");
                //moveDirection *= speed;
                transform.LookAt(transform.position + moveDirection);
                //if (Input.GetKeyDown (KeyCode.Space))	moveDirection.y = jumpSpeed;
            }
            if (moveDirection.x != 0 || moveDirection.z != 0)
                isMove = nowframe;
                
            if(nowframe - isMove < 3) {
                animator.SetInteger("attack", 0);
                animator.SetInteger("Skill2", 0);
                animator.SetInteger("Skill1", 0);
                animator.SetInteger("condition", 1);
            }
            else
                animator.SetInteger("condition", 0);
            moveDirection.y -= gravity * Time.deltaTime;
            //print ("shit");
            num.moveDirection = moveDirection;
            CollisionFlags shit = cc_test.Move(moveDirection);
        }

        void onAttack()
        {
        //print ("fuck");
            Invoke("newblade", 0.1f);
            if ((attack.IsName("Idle2") || attack.IsName("run1")))
                animator.SetInteger("attack", 1);
            else if (attack.IsName("attack1") && attack.normalizedTime > 0.65f)
                animator.SetInteger("attack", 2);
            else if (attack.IsName("attack2") && attack.normalizedTime > 0.65f)
                animator.SetInteger("attack", 3);
            else if (attack.IsName("attack3") && attack.normalizedTime > 0.65f)
                animator.SetInteger("attack", 4);
        }
        void Skill1()
        {
            animator.SetInteger("condition", 0);
            animator.SetInteger("Skill1", 1);
            Invoke("newBullet", 0.8f);
        }
        void Skill2()
        {
            animator.SetInteger("Skill2", 1);
            Invoke("newBuff", 0.8f);
        }
        void Skill3()
        {
            GameObject obj = Instantiate(skillPrefab[3] as GameObject);
            obj.GetComponent<boomController>().setPlayer(this.GetComponent<Numeric>());
            obj.transform.rotation = transform.rotation;
            obj.transform.position = transform.position;
    }
        void newblade() {
            //Debug.Log("fuck");
            GameObject obj = Instantiate(skillPrefab[0] as GameObject);
            obj.GetComponent<AttackController>().setPlayer(this.GetComponent<Numeric>());
            obj.transform.rotation = transform.rotation;
            obj.transform.position = transform.position;
        }
        void newBuff()
        {
            num.HP += 50;
            GameObject obj = Instantiate(skillPrefab[2] as GameObject);
            obj.transform.rotation = transform.rotation;
            obj.transform.position = transform.position;
            if (num.HP > num.initHP)
                num.HP = num.initHP;
        }
        void newBullet()
        {
            GameObject obj = Instantiate(skillPrefab[1] as GameObject);
            obj.GetComponent<SkillController>().setPlayer(this.GetComponent<Numeric>());
            //GameObject player = GameObject.FindWithTag ("Player");
            obj.transform.rotation = transform.rotation;
            obj.transform.position = transform.position;
        }
        void Put_onAttack()
        {
            Message shit = new Message()
            {
                type = 4,
                Type = num.id.ToString(),
                v3 = moveDirection
            };
            SendThread.put(shit);
        }
        void Put_Skill1()
        {
            Message shit = new Message()
            {
                type = 5,
                Type = num.id.ToString(),
                v3 = moveDirection
            };
            SendThread.put(shit);
        }
        void Put_Skill2()
        {
            Message shit = new Message()
            {
                type = 6,
                Type = num.id.ToString(),
                v3 = moveDirection
            };
            SendThread.put(shit);
        }
        void Put_Skill3()
        {
            Message shit = new Message()
            {
                type = 7,
                Type = num.id.ToString(),
                v3 = moveDirection
            };
            SendThread.put(shit);
        }
    }

