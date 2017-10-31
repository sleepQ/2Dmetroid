using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour {
	public GameObject shot;
	private float delay;
	private float rate = 0.6f;
	private float hp;
	public Image currentHP;
	public Text ratioText;
	private float maxHP;
	private float speed = 1.5f;
	private float direction;
	public GameObject deadParticle;
	void Start(){
		direction = 1;
		delay = Time.time + 2f;
		hp = 100f;
		maxHP = 100f;
	}
	void Update () {
		if(!Player.isDead){
			if(delay < Time.time){
				delay = Time.time + rate;
				float rand = Random.Range(-45f,45f);
				Instantiate(shot,transform.position + new Vector3(-2,-1,0),transform.rotation * Quaternion.Euler(0,0,rand));
			}
		}
		if (transform.position.y >= 4.5f) {
			direction = -1;
		}
		else if (transform.position.y <= -2f){
			direction = 1;
		}
		Vector3 movement = transform.up * direction * speed * Time.deltaTime;
		transform.Translate(movement);
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "beam"){
			hp -= 2;
			if(hp <= 0){
				hp = 0;
				Destroy(gameObject);
				Instantiate(deadParticle,transform.position,transform.rotation);
				GameControl.instance.PlayerWon();
			}
			UpdHP();
			StartCoroutine(TakeDmg());
		}
	}
	IEnumerator TakeDmg(){
		GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(0.08f);
		GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(0.08f);
	}
	void UpdHP(){
		float ratio = hp / maxHP;
		currentHP.rectTransform.localScale = new Vector3(ratio,1,1);
		ratioText.text = (ratio*100).ToString("0")+'%';
	}
}
