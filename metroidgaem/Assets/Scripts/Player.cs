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
	void Start () {
		isDead = false;
		rightFace = false;
		anim = GetComponent<Animator>();
		rbPlayer = GetComponent<Rigidbody2D>();
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
		GetComponent<Animator>().enabled = true;
		rbPlayer.velocity = new Vector2(inputH * speed,rbPlayer.velocity.y);
		anim.SetFloat("speed",Mathf.Abs(inputH));
		if(!isGrounded){
			GetComponent<Animator>().enabled = false;
		}
		if(isGrounded && jump){
			
			jump = false;
			isGrounded = false;
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
		}
		if(Input.GetKey(KeyCode.Space) && Time.time > nextShot){
			nextShot = Time.time + fireRate;
			Instantiate(beam,shotspwn.position,shotspwn.rotation);
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
		if(rbPlayer.velocity.y <= 0){
			foreach(Transform point in groundPoints){
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRad,whatIsGround);
				for(var i=0;i<colliders.Length;i++){
					if(colliders[i].gameObject != gameObject){
						
					
						return true;
					}
				}
			}
		}
		return false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "enemy"){
			isDead = true;
			anim.SetBool("dead",true);
			GameControl.instance.PlayerDied();
			
		}
	}
	
}
