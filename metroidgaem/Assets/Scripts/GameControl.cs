using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	private AudioManager a;
	public static GameControl instance;
	public GameObject gameOverText;
	public GameObject winText;
	private bool gameOver = false;
	private bool won = false;
	void Awake(){
		a = FindObjectOfType<AudioManager>();
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
	}
	void Update () {
		if(gameOver && Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene(0);
			a.Play("music2");
		}
		if(won && Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene(0);
			a.Play("music2");
		}
	}
	public void PlayerDied(){
		gameOverText.SetActive(true);
		gameOver = true;
		a.Stop("music2");
	}
	public void PlayerWon(){
		winText.SetActive(true);
		won = true;
		a.Stop("music2");
	}
}
