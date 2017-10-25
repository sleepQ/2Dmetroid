using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
	public float speed;
    private Rigidbody2D rb;
	public GameObject particle;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
		if(Player.rightFace){
        rb.velocity = transform.right * speed;
		}else{
			rb.velocity = -transform.right * speed;
		}
    }
	void Update(){
		Destroy(gameObject,2f);
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "enemy" || other.tag == "platform"){
			Instantiate(particle,transform.position,transform.rotation);
			Destroy(gameObject);
		}
	}
	
}
