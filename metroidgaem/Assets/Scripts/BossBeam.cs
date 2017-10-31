using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour {
	public float speed;
    public Rigidbody2D rb;
	public GameObject particle;
    void Start ()
    {
        	rb.velocity = -transform.right * speed;
    }
	void Update(){
		Destroy(gameObject,2.5f);
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" || other.tag == "platform"){
			if(!Player.immortal){
				Instantiate(particle,transform.position,transform.rotation);
				Destroy(gameObject);
			}
		}
	}
}
