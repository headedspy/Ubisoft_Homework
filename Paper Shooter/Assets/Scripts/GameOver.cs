using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	private bool gameOver = false;

	public void EndGame(){
		gameOver = true;
		Camera.main.GetComponent<AudioSource> ().Stop ();
		Time.timeScale = 0;
	}

	void Update(){
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Time.timeScale = 1;
				SceneManager.LoadScene ("Menu");
			} else if (Input.GetKeyDown (KeyCode.Space)) {
				Time.timeScale = 1;
				SceneManager.LoadScene ("Main");
			}
		}
	}
}
