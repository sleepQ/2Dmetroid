using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Animator anim;
	public float speed = 5f;
	public static bool rightFace;
	private Rigidbody2D rbPlayer;
	public Transform[] groundPoints;
	public float groundRad;
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	[SerializeField]
	private float jumpForce;
	private float nextShot;
	public float fireRate;
	public Transform shotspwn;
	public Transform duckshotspwn;
	public GameObject beam;
	public static bool isDead;
	private bool dblJump;
	public GameObject jumpParticle;
	private bool ducked;
	float inputH;
	private BoxCollider2D boxcoll;
	void Awake(){
		anim = GetComponent<Animator>();
		rbPlayer = GetComponent<Rigidbody2D>();
		boxcoll = GetComponent<BoxCollider2D>();
	}
	void Start () {
		
		dblJump = false;
		isDead = false;
		rightFace = false;
	}
	// void FixedUpdate()
	// if(!isDead){
		// 	float inputH = Input.GetAxisRaw("Horizontal");
		// 	//from upd
			
		// 	isGrounded = IsGrounded();
		// 	Movement(inputH);
		// 	Flip(inputH);
		// }
	// }
	void Update () {
		if(!isDead){
			inputH = Input.GetAxisRaw("Horizontal");
			//from upd
			HandleInput();
	
			isGrounded = IsGrounded();
			Movement(inputH);
			Flip(inputH);
		}
	}
	void Movement(float inputH){
		rbPlayer.velocity = new Vector2(inputH * speed,rbPlayer.velocity.y);
		anim.SetFloat("speed",Mathf.Abs(inputH));
		if(!isGrounded && jump){
			//jump was and is here
			jump = false;
			isGrounded = false;
			anim.SetBool("jump1",true);
		}
		if(isGrounded && jump){
			//jump was not here
			jump = false;
			dblJump = true;
			Instantiate(jumpParticle,transform.position,transform.rotation);
			rbPlayer.AddForce(new Vector2(0,jumpForce));
		}		
	}
	void HandleInput(){
		ducked = anim.GetBool("duck");
		if(Input.GetKeyDown(KeyCode.W)){
			jump = true;
			if(Input.GetKeyDown(KeyCode.W) && !isGrounded && dblJump){
				dblJump = false;
					anim.SetBool("jump",true);
					Instantiate(jumpParticle,transform.position,transform.rotation);
					rbPlayer.AddForce(new Vector2(0,jumpForce/2));
			}
		}
		
		if(Input.GetKey(KeyCode.Space) && Time.time > nextShot){
			nextShot = Time.time + fireRate;
			//sound 
			AudioManager.instance.Play("shot");
			if(ducked == true){
				Instantiate(beam,duckshotspwn.position,duckshotspwn.rotation);
			}else{
				Instantiate(beam,shotspwn.position,shotspwn.rotation);
			}
		}
		if(Input.GetKey(KeyCode.S) && isGrounded){
			anim.SetBool("duck",true);
			boxcoll.offset = new Vector2(-0.5f,-1.31f);
			boxcoll.size = new Vector2(3.74f,2f);
		}
		if(Input.GetKeyUp(KeyCode.S) || inputH != 0){
			anim.SetBool("duck",false);
			boxcoll.offset = new Vector2(0.15f,0.03f);
			boxcoll.size = new Vector2(0.81f,4.56f);
		}	
	}
	void Flip(float inputH){
		if(inputH > 0 && !rightFace || inputH < 0 && rightFace){
			rightFace = !rightFace;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
	private bool IsGrounded(){
		// if its == 0 it queeus jumps after decelerating
		//if its <= 0 it has some weird mini jumps after spammin jump and landing
		if(rbPlayer.velocity.y == 0){
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRad,whatIsGround);
				for(var i=0;i<colliders.Length;i++){
					if(colliders[i].gameObject != gameObject){
						anim.SetBool("jump",false);
						anim.SetBool("jump1",false);
						
						return true;
					}
				}
			}
		}
		anim.SetBool("jump1",true);
		anim.SetBool("duck",false);
		return false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "enemy" || other.tag == "bossBeam"){
				isDead = true;
				anim.SetBool("dead",true);
				GameControl.instance.PlayerDied();
		}
		if(other.tag == "gem"){
			GameControl.instance.BossScene();
		}
	}

}
