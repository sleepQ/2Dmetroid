using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
 private float rightLimit;
 private float leftLimit;
 public float speed = 1.5f;
 private int direction;
 public Transform platform;
 private float hitsToDestroy;
 void Start(){
	 
	 direction = 1;
		if(platform.position.x > 0){
			rightLimit = (platform.position.x/2)+platform.position.x;
			leftLimit = platform.position.x - (platform.position.x/2);
		}else if(platform.position.x < 0){
			rightLimit = (-platform.position.x/2)+platform.position.x;
			leftLimit = platform.position.x - (-platform.position.x/2);	
		}else{
			rightLimit = platform.localScale.x/2;
			leftLimit = -platform.localScale.x/2;
		}
	 hitsToDestroy = 0;
 }
 
 void Update () {
	 if(hitsToDestroy == 0){
		if (transform.position.x >= rightLimit) {
			direction = -1;
		}
		else if (transform.position.x <= leftLimit){
			direction = 1;
		}
	 }else{ direction = 0;}
    Vector3 movement = Vector3.right * direction * speed * Time.deltaTime; 
    transform.Translate(movement);
  }
  void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "beam"){
			hitsToDestroy++;
			 GetComponent<SpriteRenderer>().color = Color.yellow;
			 GetComponent<Animator>().enabled = false;
			if(hitsToDestroy == 2){
				Destroy(gameObject);
			}
		}
	}
}
