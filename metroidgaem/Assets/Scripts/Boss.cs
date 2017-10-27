using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	public GameObject shot;
	private float delay;
	private float rate = 0.7f;
	private float hp;
	void Start(){
		delay = Time.time + 1;
		hp = 100f;
	}
	void Update () {
		if(delay < Time.time){
			delay = Time.time + rate;
			float rand = Random.Range(-45f,45f);
				Instantiate(shot,transform.position + new Vector3(-2,-1,0),transform.rotation * Quaternion.Euler(0,0,rand));
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "beam"){
			hp--;
			if(hp <= 0){
				Destroy(gameObject);
				GameControl.instance.PlayerWon();
			}
			StartCoroutine(TakeDmg());
		}
	}
	IEnumerator TakeDmg(){
		GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(0.08f);
		GetComponent<SpriteRenderer>().enabled = true;
	}
}
