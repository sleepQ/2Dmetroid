using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {
	public float t=2f;
	void Start () {
		Destroy(gameObject,t);
	}
}
