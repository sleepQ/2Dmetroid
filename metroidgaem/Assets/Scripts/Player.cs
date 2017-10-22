using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Animator anim;
	public float speed = 5f;
	public static bool rightFace;
	private Rigidbody2D rbPlayer;
	private float screenHalfWidth;
    private float playerW;
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
	public GameObject beam;
	public static bool isDead;
	private bool dblJump;
	public GameObject jumpParticle;
	private AudioManager a;
	private bool playedSound;
	void Awake(){
		a = FindObjectOfType<AudioManager>();
		anim = GetComponent<Animator>();
		rbPlayer = GetComponent<Rigidbody2D>();
	}
	void Start () {
		playedSound = false;
		dblJump = false;
		isDead = false;
		rightFace = false;
		playerW = transform.localScale.x;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
	}
	void Update()
	{
		if(!isDead){
			HandleInput();
		}
	}
	void FixedUpdate () {
		if(!isDead){
			float inputH = Input.GetAxisRaw("Horizontal");
			isGrounded = IsGrounded();
			Movement(inputH);
			Flip(inputH);
		}
	}
	void Movement(float inputH){
		rbPlayer.velocity = new Vector2(inputH * speed,rbPlayer.velocity.y);
		anim.SetFloat("speed",Mathf.Abs(inputH));
		if(!isGrounded && jump){
			jump = false;
			isGrounded = false;
			anim.SetBool("jump1",true);
		}
		if(isGrounded && jump){
			dblJump = true;
			Instantiate(jumpParticle,transform.position,transform.rotation);
			rbPlayer.AddForce(new Vector2(0,jumpForce));
		}
		if (transform.position.x < -screenHalfWidth + playerW) {
            transform.position = new Vector2(-screenHalfWidth + playerW, transform.position.y);
        }
        if (transform.position.x > screenHalfWidth - playerW) {
            transform.position = new Vector2(screenHalfWidth - playerW, transform.position.y);
        }
		
	}
	void HandleInput(){
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
			Instantiate(beam,shotspwn.position,shotspwn.rotation);
			a.Play("shot");
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
		
		return false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "enemy"){
			isDead = true;
			anim.SetBool("dead",true);
			GameControl.instance.PlayerDied();
			if(!playedSound){
				a.Play("lose");
				playedSound = true;
			}
		}
	}
	
}
