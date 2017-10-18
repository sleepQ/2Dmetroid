using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
	public float speed;
	private float camW;
    private Rigidbody2D rb;
    void Start ()
    {
		camW = Camera.main.aspect * Camera.main.orthographicSize;
        rb = GetComponent<Rigidbody2D>();
		if(Player.rightFace){
        rb.velocity = transform.right * speed;
		}else{
			rb.velocity = -transform.right * speed;
		}
    }
	void Update(){
		if(transform.position.x > camW || transform.position.x < -camW){
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "enemy" || other.tag == "platform"){
			Destroy(gameObject);
		}
	}
	
}
