﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static GameControl instance;
	public GameObject gameOverText;
	public GameObject winText;
	private bool gameOver;
	public static bool won;
	private bool soundPlayed;
	void Awake(){
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
	}
	void Start(){
		soundPlayed = false;
		won = false;
		gameOver = false;
	}
	void Update () {
		if(gameOver && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene("scene");
			AudioManager.instance.Play("music2");
			winText.SetActive(false);
			gameOverText.SetActive(false);
		}
		if(won && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene("scene");
			AudioManager.instance.Play("music2");
			winText.SetActive(false);
			gameOverText.SetActive(false);
		}
	}
	public void PlayerDied(){
		gameOverText.SetActive(true);
		gameOver = true;
		Boss.bossImmortal = true;
		if(SceneManager.GetActiveScene().name == "scene"){
			AudioManager.instance.Stop("music2");
		}
		else if(SceneManager.GetActiveScene().name == "boss"){
			AudioManager.instance.Stop("music");
		}
		if(!soundPlayed){
			soundPlayed = true;
			AudioManager.instance.Play("lose");
		}
	}
	public void PlayerWon(){
		winText.SetActive(true);
		won = true;
		Player.immortal = true;
		AudioManager.instance.Stop("music");
		AudioManager.instance.Play("win");
	}
	public void BossScene(){
		SceneManager.LoadScene("boss");
		AudioManager.instance.Stop("music2");
		AudioManager.instance.Play("music");
	}
}
