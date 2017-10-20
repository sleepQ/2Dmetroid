using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos : MonoBehaviour {

	public float yMin;
	public float yMax;
	private Transform target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform;
	}
	
	void LateUpdate () {
		if(!Player.isDead){
			transform.position = new Vector3(transform.position.x,Mathf.Clamp(target.position.y,yMin,yMax),transform.position.z);
		}
	}
}
