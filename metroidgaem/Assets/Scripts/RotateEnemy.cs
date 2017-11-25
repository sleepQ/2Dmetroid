using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour {
 private float rightLimit;
 private float leftLimit;
 public float speed;
 private int direction;
 public Transform platform;
 private float hitsToDestroy;
 void Start(){
	 direction = 1;
	 speed = 1.5f;
	 hitsToDestroy = 0;
 }
 
 void Update () {
    Vector2 movement = Vector2.right * direction * speed * Time.deltaTime; 
    transform.Translate(movement);
  }
  void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "beam"){
			hitsToDestroy++;
			direction = 0;
			GetComponent<SpriteRenderer>().color = Color.yellow;
			GetComponent<Animator>().enabled = false;
			if(hitsToDestroy == 2){
				Destroy(gameObject);
			}
		}
		float i = 1;
		if(other.tag == "rotate"){
			RotateE(i);
			i++;
			if(i > 4){
				i = 1;
			}
		}
	}
	public void RotateE(float x){
		transform.Rotate(0,0,-90*x);
	}
}
