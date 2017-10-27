using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpboss : MonoBehaviour {
	public Image currentHP;
	public Text ratioText;
	private float hpoints = 100;
	private float maxHP = 100;
	void Start(){
		UpdHP();
	}
	void UpdHP(){
		float ratio = hpoints / maxHP;
		currentHP.rectTransform.localScale = new Vector3(ratio,1,1);
		ratioText.text = (ratio*100).ToString("0")+'%';
	}
	void TakeDemage(float dmg){
		hpoints -= dmg;
		if(hpoints <= 0){
			hpoints = 0;
		}
		UpdHP();
	}
}
