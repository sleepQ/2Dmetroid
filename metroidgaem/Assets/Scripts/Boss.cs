using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour {
	public GameObject shot;
	private float delay;
	private float rageRate;
	private float rate;
	private float hp;
	public Image currentHP;
	public Text ratioText;
	private float maxHP;
	private float speed;
	private float direction;
	public GameObject deadParticle;
	public static bool bossImmortal;
	private Animator anim;
	private float beamDmg;
	private float supaBeamDmg;
	void Start(){
		anim = GetComponent<Animator>();
		direction = 1;
		delay = Time.time + 2f;
		hp = 100f;
		maxHP = 100f;
		bossImmortal = false;
		rate = 0.6f;
		rageRate = 1.2f;
		speed = 1.5f;
		beamDmg = 2f;
		supaBeamDmg = 15f;
	}
	void Update () {
		if(hp > maxHP/2){
			anim.SetBool("rage",false);
			if(!Player.isDead){
				if(delay < Time.time){
					delay = Time.time + rate;
					float rand = Random.Range(-45f,45f);
					Instantiate(shot,transform.position + new Vector3(-2,-1,0),transform.rotation * Quaternion.Euler(0,0,rand));
				}
			}
		}
		if(hp <= maxHP/2){
			anim.SetBool("rage",true);
			if(!Player.isDead){
				if(delay < Time.time){
					delay = Time.time + rageRate;
					for(int i=1;i<19;i++){
						Instantiate(shot,transform.position + new Vector3(-2,-1,0),transform.rotation * Quaternion.Euler(0,0,i*20));
					}
				}
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
		if(!bossImmortal){
			if(other.tag == "beam"){
				hp -= beamDmg;
				if(hp <= 0){
					hp = 0;
					Destroy(gameObject);
					Instantiate(deadParticle,transform.position,transform.rotation);
					GameControl.instance.PlayerWon();
				}
				UpdHP();
				StartCoroutine(TakeDmg());
			}
			if(other.tag == "supaBeam"){
				hp -= supaBeamDmg;
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
