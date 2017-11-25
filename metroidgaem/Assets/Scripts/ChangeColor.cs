using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	float timeLeft;
	Color targetColor;
	Renderer target;
	void Start(){
		target = GetComponent<Renderer>();
	}
	void Update()
	{
		if (timeLeft <= Time.deltaTime)
		{
			target.material.color = targetColor;
			targetColor = new Color(Random.value, Random.value, Random.value);
			timeLeft = 3f;
		}
		else
		{
			target.material.color = Color.Lerp(target.material.color, targetColor, Time.deltaTime / timeLeft);
			timeLeft -= Time.deltaTime;
		}
	}
}
