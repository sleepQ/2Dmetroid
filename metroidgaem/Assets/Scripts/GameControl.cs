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
	private bool gameOver;
	private bool won;
	private bool soundPlayed = false;
	void Awake(){
		a = FindObjectOfType<AudioManager>();
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
	}
	void Start(){
		won = false;
		gameOver = false;
	}
	void Update () {
		if(gameOver && Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene("scene");
			a.Play("music2");
		}
		if(won && Input.GetKeyDown(KeyCode.Space)){
			SceneManager.LoadScene("scene");
			a.Play("music2");
		}
	}
	public void PlayerDied(){
			gameOverText.SetActive(true);
			gameOver = true;
			if(SceneManager.GetActiveScene().name == "scene"){
				a.Stop("music2");
			}
			else if(SceneManager.GetActiveScene().name == "boss"){
				a.Stop("music");
			}
			if(!soundPlayed){
				soundPlayed = true;
				a.Play("lose");
			}
	}
	public void PlayerWon(){
		winText.SetActive(true);
		won = true;
		a.Stop("music");
		a.Play("win");
	}
	public void BossScene(){
		SceneManager.LoadScene("boss");
		a.Stop("music2");
		a.Play("music");
	}
	public void Shot(){
		a.Play("shot");
	}
}
