using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static GameControl instance;
	public GameObject gameOverText;
	private bool gameOver = false;
	void Awake(){
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
	}
	void Update () {
		if(gameOver && Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene(0);
		}
	}
	public void PlayerDied(){
		gameOverText.SetActive(true);
		gameOver = true;
	}
}
